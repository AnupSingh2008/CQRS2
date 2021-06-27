using Shop.Common.RabbitMq;
using Shop.Common.Messages;
using System.Threading.Tasks;

namespace Shop.Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}