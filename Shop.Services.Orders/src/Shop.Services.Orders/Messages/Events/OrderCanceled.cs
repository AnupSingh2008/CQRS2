using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Shop.Common.Messages;

namespace Shop.Services.Orders.Messages.Events
{
    public class OrderCanceled : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public IDictionary<Guid, int> Products { get; }

        [JsonConstructor]
        public OrderCanceled(Guid id, Guid customerId, IDictionary<Guid, int> products)
        {
            Id = id;
            CustomerId = customerId;
            Products = products;
        }
    }
}
