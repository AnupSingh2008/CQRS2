using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Customers.Messages.Commands;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Customers
{
    public class ClearCartHandler : ICommandHandler<ClearCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICartsRepository _cartsRepository;

        public ClearCartHandler(IBusPublisher busPublisher,
            ICartsRepository cartsRepository)
        {
            _busPublisher = busPublisher;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(ClearCart command, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.Clear();
            await _cartsRepository.UpdateAsync(cart);
            await _busPublisher.PublishAsync(new CartCleared(command.CustomerId), context);
        }
    }
}