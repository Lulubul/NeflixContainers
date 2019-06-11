using Recommendation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommendation.API.Application
{
    public interface IRecommendationsService
    {
        Task<List<Movie>> GetVideoRecommendationsByUser(Guid userId, Guid profileId);
    }

    public class RecommendationsService : IRecommendationsService
    {
        //private readonly IHistoryService _historyService;
        //private readonly IMovieService _movieService;

        public Task<List<Movie>> GetVideoRecommendationsByUser(Guid userId, Guid profileId)
        {
            throw new NotImplementedException();
        }

        private UserPreferences GetTopGenresAndReleaseYears(List<Movie> movies)
        {
            var moviesByGenreDictionary = new Dictionary<string, List<Movie>>();
            var moviesByReleaseYearDictionary = new Dictionary<string, List<Movie>>();

            foreach (var movie in movies)
            {
                if (!string.IsNullOrEmpty(movie.Genres))
                {
                    var genres = movie.Genres.Split(',').ToList();
                    genres.ForEach((genre) =>
                    {
                        if (moviesByGenreDictionary.ContainsKey(genre))
                        {
                            moviesByGenreDictionary[genre].Add(movie);
                        }
                        else
                        {
                            moviesByGenreDictionary.Add(genre, new List<Movie> { movie });
                        }
                    });
                }

                if (moviesByReleaseYearDictionary.ContainsKey(movie.ReleaseYear))
                {
                    moviesByReleaseYearDictionary[movie.ReleaseYear].Add(movie);
                }
                else
                {
                    moviesByReleaseYearDictionary.Add(movie.ReleaseYear, new List<Movie> { movie });
                }
            }
            return new UserPreferences(moviesByGenreDictionary, moviesByReleaseYearDictionary);
        }
    }


}