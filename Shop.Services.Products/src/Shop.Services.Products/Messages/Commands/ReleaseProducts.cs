using System;
using System.Collections.Generic;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Products.Messages.Commands
{
    public class ReleaseProducts : ICommand
    {
        public Guid OrderId { get; set; }
        public IDictionary<Guid, int> Products { get; }
        
        [JsonConstructor]
        public ReleaseProducts(Guid orderId, IDictionary<Guid, int> products)
        {
            OrderId = orderId;
            Products = products;
        }
    }
}