using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    public class ProductAddedToCart : IEvent
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public ProductAddedToCart(Guid customerId, Guid productId,
            int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }        
    }
}