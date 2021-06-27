using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Customers.Domain;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Products
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IProductsRepository _productsRepository;

        public ProductCreatedHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ProductCreated @event, ICorrelationContext context)
        {
            var product = new Product(@event.Id, @event.Name, @event.Price, @event.Quantity);
            await _productsRepository.AddAsync(product);
        }
    }
}