using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
