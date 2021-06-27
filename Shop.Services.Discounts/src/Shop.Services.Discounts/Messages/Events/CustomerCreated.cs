using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Discounts.Messages.Events
{
    [MessageNamespace("customers")]
    public class CustomerCreated : IEvent
    {
        public Guid Id { get; }
        public string Email { get; }

        [JsonConstructor]
        public CustomerCreated(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}