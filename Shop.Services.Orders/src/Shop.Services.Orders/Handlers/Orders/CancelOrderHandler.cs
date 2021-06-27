using System.Linq;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Orders.Messages.Commands;
using Shop.Services.Orders.Messages.Events;
using Shop.Services.Orders.Repositories;
using System.Threading.Tasks;

namespace Shop.Services.Orders.Handlers.Orders
{
    public sealed class CancelOrderHandler : ICommandHandler<CancelOrder>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IOrdersRepository _ordersRepository;

        public CancelOrderHandler(IBusPublisher busPublisher,
            IOrdersRepository ordersRepository)
        {
            _busPublisher = busPublisher;
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(CancelOrder command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order == null || order.CustomerId != command.CustomerId)
            {
                throw new ShopException("order_not_found", $"Order with id: '{command.Id}' " +
                                                            $"was not found for customer with id: '{command.CustomerId}'.");
            }

            order.Cancel();
            await _ordersRepository.UpdateAsync(order);
            await _busPublisher.PublishAsync(new OrderCanceled(command.Id, command.CustomerId,
                order.Items.ToDictionary(i => i.Id, i => i.Quantity)), context);
        }
    }
}
