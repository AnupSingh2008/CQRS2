using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Products.Messages.Events
{
    public class ProductDeleted : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ProductDeleted(Guid id)
        {
            Id = id;
        }
    }
}
