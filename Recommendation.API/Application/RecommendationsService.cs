using AutoMapper.Configuration;
using Recommendation.API.Shared;
using Recommendation.Domain;
using Recommendation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Recommendation.API.Application
{
    public interface IRecommendationsService
    {
        Task<List<Movie>> GetVideoRecommendationsByUser(Guid userId, Guid profileId);
    }

    public class RecommendationsService : IRecommendationsService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IHttpClientFactory _clientFactory;

        public RecommendationsService(IRecommendationRepository recommendationRepository, IHttpClientFactory clientFactory)
        {
            _recommendationRepository = recommendationRepository;
            _clientFactory = clientFactory;
        }

        public async Task<List<Movie>> GetVideoRecommendationsByUser(Guid userId, Guid profileId)
        {
            var userStatistics = await _recommendationRepository.GetUserStatistics(userId.ToString(), profileId.ToString());
            if (userStatistics == null)
            {
                return new List<Movie>();
            }

            var genresPreferences = TopPreferences(userStatistics.GenresPreferences);
            var relaseYearPreferences = TopPreferences(userStatistics.RelaseYearPreferences);

            if (string.IsNullOrEmpty(genresPreferences) || string.IsNullOrEmpty(relaseYearPreferences))
            {
                return new List<Movie>();
            }
            var request = new HttpRequestMessage(HttpMethod.Get, $"{relaseYearPreferences}/{genresPreferences}");
            var client = _clientFactory.CreateClient("movies");
            var response = await client.SendAsync(request);
            var movies = new List<Movie>();
            if (response.IsSuccessStatusCode)
            {
                movies = await response.Content.ReadAsAsync<List<Movie>>();
                var videoHistory = userStatistics.VideoIdPreferences.Split(",");
                movies = movies.Where((x) => !videoHistory.Any((id) => id == x.VideoId)).ToList();
            }
            return movies;
        }

        private string TopPreferences(string userPreferences)
        {
            return userPreferences.ConvertToDictionary().OrderBy(x => x.Value).Select(x => x.Key).FirstOrDefault();
        }
    }
}