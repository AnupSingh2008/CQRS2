using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    public class ProductDeletedFromCart : IEvent
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }

        [JsonConstructor]
        public ProductDeletedFromCart(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }        
    }
}