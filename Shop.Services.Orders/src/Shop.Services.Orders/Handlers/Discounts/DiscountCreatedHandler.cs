
using Microsoft.Extensions.Logging;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Orders.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services.Orders.Handlers.Discounts
{
    public class DiscountCreatedHandler :IEventHandler<DiscountCreated>
    {
        private readonly ILogger<DiscountCreatedHandler> _logger;
        public DiscountCreatedHandler(ILogger<DiscountCreatedHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleAsync(DiscountCreated @event,ICorrelationContext context)
        {
            _logger.LogInformation($"Discount Created : {@event.Id}");
            return Task.CompletedTask;
        }
    }
}
