using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Products.Messages.Events
{
    public class ReserveProductsRejected : IRejectedEvent
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ReserveProductsRejected(Guid orderId, string reason, string code)
        {
            OrderId = orderId;
            Reason = reason;
            Code = code;
        }
    }
}