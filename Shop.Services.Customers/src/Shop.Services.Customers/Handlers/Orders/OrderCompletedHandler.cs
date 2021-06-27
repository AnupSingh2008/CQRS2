using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Orders
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public OrderCompletedHandler(ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(OrderCompleted @event, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(@event.CustomerId);
            cart.Clear();
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}