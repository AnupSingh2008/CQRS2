using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Orders
{
    public class OrderCanceledHandler : IEventHandler<OrderCanceled>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public OrderCanceledHandler(ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(OrderCanceled @event, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(@event.CustomerId);
            foreach (var cartItem in cart.Items)
            {
                var product = await _productsRepository.GetAsync(cartItem.ProductId);
                if (product == null)
                {
                    continue;
                }

                product.SetQuantity(product.Quantity + cartItem.Quantity);
                await _productsRepository.UpdateAsync(product);
            }
        }
    }
}