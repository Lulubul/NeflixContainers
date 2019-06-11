using EventBusRabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommendation.API.IntegrationEvents
{

    public interface IRecommandationIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }

    public class RecommandationIntegrationEventService : IRecommandationIntegrationEventService
    {
        public Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            throw new NotImplementedException();
        }
    }
}
