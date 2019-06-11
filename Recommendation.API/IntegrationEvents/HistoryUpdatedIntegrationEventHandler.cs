using System.Threading.Tasks;
using EventBusRabbitMQ;
using Recommendation.Infrastructure;

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
                    GenresPreferences = @event.Genres,
                    RelaseYearPreferences = @event.ReleaseYear
                };
                await _recommendationRepository.AddUserStatistics(newUserStatistics);
            }
            else
            {
                userStatistics.GenresPreferences += ";" + @event.Genres;
                userStatistics.RelaseYearPreferences += ";" + @event.ReleaseYear;
                await _recommendationRepository.UpdateUserStatistics(userStatistics);
            }
        }
    }
}
