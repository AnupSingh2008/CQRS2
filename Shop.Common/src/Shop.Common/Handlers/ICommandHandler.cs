using Shop.Common.RabbitMq;
using Shop.Common.Messages;
using System.Threading.Tasks;

namespace Shop.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}