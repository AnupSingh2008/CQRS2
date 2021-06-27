using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Commands
{
    public class AddProductToCart : ICommand
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public AddProductToCart(Guid customerId, Guid productId,
            int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}