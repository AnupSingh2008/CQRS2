using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    [MessageNamespace("orders")]
    public class OrderApproved : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public OrderApproved(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}