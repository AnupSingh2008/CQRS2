using System.Linq;
using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Customers.Domain;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Products
{
    public class ProductUpdatedHandler : IEventHandler<ProductUpdated>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public ProductUpdatedHandler(ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ProductUpdated @event, ICorrelationContext context)
        {
            var product = new Product(@event.Id, @event.Name, @event.Price, @event.Quantity);
            await _productsRepository.UpdateAsync(product);
            var carts = await _cartsRepository.GetAllWithProduct(product.Id)
                .ContinueWith(t => t.Result.ToList());
            foreach (var cart in carts)
            {
                cart.UpdateProduct(product);
            }

            await _cartsRepository.UpdateManyAsync(carts);
        }
    }
}