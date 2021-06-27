using Newtonsoft.Json;
using System;
using Shop.Common.Messages;

namespace Shop.Services.Orders.Messages.Commands
{
    public class CreateOrder : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CreateOrder(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
