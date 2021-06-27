using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Customers.Messages.Commands;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Customers
{
    public class AddProductToCartHandler : ICommandHandler<AddProductToCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public AddProductToCartHandler(IBusPublisher busPublisher,
            ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _busPublisher = busPublisher;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(AddProductToCart command, ICorrelationContext context)
        {
            if (command.Quantity <= 0)
            {
                throw new ShopException(Codes.InvalidQuantity,
                    $"Invalid quantity: '{command.Quantity}'.");
            }

            var product = await _productsRepository.GetAsync(command.ProductId);
            if (product == null)
            {
                throw new ShopException(Codes.ProductNotFound,
                    $"Product: '{command.ProductId}' was not found.");
            }

            if (product.Quantity < command.Quantity)
            {
                throw new ShopException(Codes.NotEnoughProductsInStock,
                    $"Not enough products in stock: '{command.ProductId}'.");
            }

            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.AddProduct(product, command.Quantity);
            await _cartsRepository.UpdateAsync(cart);
            await _busPublisher.PublishAsync(new ProductAddedToCart(command.CustomerId,
                command.ProductId, command.Quantity), context);
        }
    }
}