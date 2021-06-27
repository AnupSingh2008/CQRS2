using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    public class ClearCartRejected : IRejectedEvent
    {
        public Guid CustomerId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ClearCartRejected(Guid customerId, string reason, string code)
        {
            CustomerId = customerId;
            Reason = reason;
            Code = code;
        }
    }
}