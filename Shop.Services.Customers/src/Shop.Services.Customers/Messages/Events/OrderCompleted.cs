using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    [MessageNamespace("orders")]
    public class OrderCompleted : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public OrderCompleted(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
