using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    [MessageNamespace("products")]
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
