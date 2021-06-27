using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    public class CartCleared : IEvent
    {
        public Guid CustomerId { get; }

        [JsonConstructor]
        public CartCleared(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}