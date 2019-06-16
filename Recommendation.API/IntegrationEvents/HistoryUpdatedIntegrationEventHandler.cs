using System.Threading.Tasks;
using EventBusRabbitMQ;
using Recommendation.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Recommendation.API.Shared;

namespace Recommendation.API.IntegrationEvents
{
    public class HistoryUpdatedIntegrationEventHandler : IIntegrationEventHandler<HistoryUpdatedIntegrationEvent>
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public HistoryUpdatedIntegrationEventHandler(IRecommendationRepository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }

        public async Task Handle(HistoryUpdatedIntegrationEvent @event)
        {
            var userStatistics = await _recommendationRepository.GetUserStatistics(@event.UserId, @event.ProfileId);
            if (userStatistics == null)
            {
                var newUserStatistics = new UserStatisticsEntity()
                {
                    PartitionKey = @event.UserId,
                    RowKey = @event.ProfileId,
                    GenresPreferences = $"{@event.Genres}:1",
                    RelaseYearPreferences = $"{@event.ReleaseYear}:1",
                    VideoIdPreferences = @event.WatchingItemId
                };
                await _recommendationRepository.AddUserStatistics(newUserStatistics);
            }
            else
            {
                var genreDictionary = userStatistics.GenresPreferences.ConvertToDictionary();
                var releaseYearDictionary = userStatistics.RelaseYearPreferences.ConvertToDictionary();
                IncrementValue(genreDictionary, @event.Genres);
                IncrementValue(releaseYearDictionary, @event.ReleaseYear);
                userStatistics.GenresPreferences = genreDictionary.ConvertToString();
                userStatistics.RelaseYearPreferences = releaseYearDictionary.ConvertToString();
                userStatistics.VideoIdPreferences = userStatistics.VideoIdPreferences + "," + @event.WatchingItemId;
                await _recommendationRepository.UpdateUserStatistics(userStatistics);
            }
        }

        private void IncrementValue(Dictionary<string, int> valuePairs, string key)
        {
            if (valuePairs.ContainsKey(key))
            {
                valuePairs[key] = valuePairs[key] + 1;
            }
            else
            {
                valuePairs[key] = 1;
            }
        }
    }
}
