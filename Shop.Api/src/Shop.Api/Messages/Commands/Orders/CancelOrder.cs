using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Api.Messages.Commands.Orders
{
    [MessageNamespace("orders")]
    public class CancelOrder : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CancelOrder(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
