using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Orders.Messages.Events
{
    public class CancelOrderRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CancelOrderRejected(Guid id, Guid customerId, string reason, string code)
        {
            Id = id;
            CustomerId = customerId;
            Reason = reason;
            Code = code;
        }
    }
}