using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Discounts.Messages.Events
{
    // Immutable
    public class DiscountCreated : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string Code { get; }
        public double Percentage { get; }

        [JsonConstructor]
        public DiscountCreated(Guid id, Guid customerId,
            string code, double percentage)
        {
            Id = id;
            CustomerId = customerId;
            Code = code;
            Percentage = percentage;
        }
    }
}