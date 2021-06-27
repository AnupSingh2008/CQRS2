using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Api.Messages.Commands.Customers
{
    [MessageNamespace("customers")]
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