using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Orders.Messages.Commands
{
    public class RevokeOrder : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public RevokeOrder(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}