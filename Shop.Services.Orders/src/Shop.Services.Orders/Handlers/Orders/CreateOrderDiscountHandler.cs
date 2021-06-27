using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Orders.Messages.Commands;
using System;
using System.Threading.Tasks;

namespace Shop.Services.Orders.Handlers.Orders
{
    public sealed class CreateOrderDiscountHandler : ICommandHandler<CreateOrderDiscount>
    {
        public Task HandleAsync(CreateOrderDiscount command, ICorrelationContext context)
        {
            Console.WriteLine($"Creating an order discount, value: '{command.Percentage}%'");
            return Task.CompletedTask;
        }
    }
}
