using System.Linq;
using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Customers.Messages.Commands;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Customers
{
    public class DeleteProductFromCartHandler : ICommandHandler<DeleteProductFromCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICartsRepository _cartsRepository;

        public DeleteProductFromCartHandler(IBusPublisher busPublisher,
            ICartsRepository cartsRepository)
        {
            _busPublisher = busPublisher;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(DeleteProductFromCart command, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.DeleteProduct(command.ProductId);
            await _cartsRepository.UpdateAsync(cart);
            await _busPublisher.PublishAsync(new ProductDeletedFromCart(command.CustomerId,
                command.ProductId), context);
        }
    }
}