using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Orders.Messages.Commands;
using Shop.Services.Orders.Messages.Events;
using Shop.Services.Orders.Repositories;

namespace Shop.Services.Orders.Handlers.Orders
{
    public class ApproveOrderHandler : ICommandHandler<ApproveOrder>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IOrdersRepository _ordersRepository;

        public ApproveOrderHandler(IBusPublisher busPublisher, IOrdersRepository ordersRepository)
        {
            _busPublisher = busPublisher;
            _ordersRepository = ordersRepository;
        }

        public async Task HandleAsync(ApproveOrder command, ICorrelationContext context)
        {
            var order = await _ordersRepository.GetAsync(command.Id);
            if (order == null)
            {
                throw new ShopException("order_not_found",
                    $"Order with id: '{command.Id}' was not found.");
            }

            order.Approve();
            await _ordersRepository.UpdateAsync(order);
            await _busPublisher.PublishAsync(new OrderApproved(command.Id, order.CustomerId), context);
        }
    }
}