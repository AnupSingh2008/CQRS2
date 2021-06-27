using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Api.Messages.Commands.Customers
{
    [MessageNamespace("customers")]
    public class DeleteProductFromCart : ICommand
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }

        [JsonConstructor]
        public DeleteProductFromCart(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}