using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Commands
{
    public class ClearCart : ICommand
    {
        public Guid CustomerId { get; }

        [JsonConstructor]
        public ClearCart(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}