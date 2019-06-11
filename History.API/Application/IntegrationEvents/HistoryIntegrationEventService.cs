using EventBusRabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace History.API.Application.IntegrationEvents
{
    public interface IHistoryIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }

    public class HistoryIntegrationEventService : IHistoryIntegrationEventService
    {
        private readonly IEventBus _eventBus;

        public HistoryIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);
            return Task.CompletedTask;
        }
    }
}
