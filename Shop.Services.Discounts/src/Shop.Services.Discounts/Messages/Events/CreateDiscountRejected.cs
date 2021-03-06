using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Discounts.Messages.Events
{
    public class CreateDiscountRejected : IRejectedEvent
    {
        public Guid CustomerId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateDiscountRejected(Guid customerId, string reason, string code)
        {
            CustomerId = customerId;
            Reason = reason;
            Code = code;
        }
    }
}