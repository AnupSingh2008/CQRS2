using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Orders.Messages.Commands;
using Shop.Services.Orders.Messages.Events;
using Shop.Services.Orders.Repositories;

namespace Shop.Services.Orders.Handlers.Orders
{
    public class RevokeOrderHandler : ICommandHandler<RevokeOrder>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IOrdersRepository _ordersRepository;

        public RevokeOrderHandler(IBusPublisher busPublisher,
            IOrdersRepository ordersRepository)
        {
            _busPublisher = busPublisher;
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(RevokeOrder command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order == null || order.CustomerId != command.CustomerId)
            {
                throw new ShopException("order_not_found", $"Order with id: '{command.Id}' " +
                                                            $"was not found for customer with id: '{command.CustomerId}'.");
            }

            order.Revoke();
            await _ordersRepository.UpdateAsync(order);
            await _busPublisher.PublishAsync(new OrderRevoked(command.Id, command.CustomerId), context);
        }
    }
}