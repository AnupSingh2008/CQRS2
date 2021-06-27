using System.Linq;
using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Products
{
    public class ProductDeletedHandler : IEventHandler<ProductDeleted>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public ProductDeletedHandler(ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ProductDeleted @event, ICorrelationContext context)
        {
            await _productsRepository.DeleteAsync(@event.Id);
            var carts = await _cartsRepository.GetAllWithProduct(@event.Id)
                .ContinueWith(t => t.Result.ToList());
            foreach (var cart in carts)
            {
                cart.DeleteProduct(@event.Id);
            }

            await _cartsRepository.UpdateManyAsync(carts);
        }
    }
}