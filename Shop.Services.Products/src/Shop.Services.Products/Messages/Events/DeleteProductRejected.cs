using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Products.Messages.Events
{
    public class DeleteProductRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public DeleteProductRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}