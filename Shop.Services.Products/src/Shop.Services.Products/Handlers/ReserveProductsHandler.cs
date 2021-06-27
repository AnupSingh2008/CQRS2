using System;
using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Products.Messages.Commands;
using Shop.Services.Products.Messages.Events;
using Shop.Services.Products.Repositories;
using Microsoft.Extensions.Logging;

namespace Shop.Services.Products.Handlers
{
    public class ReserveProductsHandler : ICommandHandler<ReserveProducts>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IProductsRepository _productsRepository;
        private readonly ILogger<ReserveProductsHandler> _logger;

        public ReserveProductsHandler(IBusPublisher busPublisher,
            IProductsRepository productsRepository,
            ILogger<ReserveProductsHandler> logger)
        {
            _busPublisher = busPublisher;
            _productsRepository = productsRepository;
            _logger = logger;
        }

        public async Task HandleAsync(ReserveProducts command, ICorrelationContext context)
        {
            foreach ((Guid productId, int quantity) in command.Products)
            {
                _logger.LogInformation($"Reserving a product: '{productId}' ({quantity})");
                var product = await _productsRepository.GetAsync(productId);
                if (product == null)
                {
                    _logger.LogInformation($"Product was not found: '{productId}' (can't reserve).");

                    continue;
                }

                product.SetQuantity(product.Quantity - quantity);
                await _productsRepository.UpdateAsync(product);
                _logger.LogInformation($"Reserved a product: '{productId}' ({quantity})");
            }

            await _busPublisher.PublishAsync(new ProductsReserved(command.OrderId,
                command.Products), context);
        }
    }
}