using Newtonsoft.Json;
using Shop.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Shop.Api.Messages.Commands.Discount
{
    [MessageNamespace("discounts")]
    public class CreateDiscount : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string Code { get; }
        public double Percentage { get; }

        [JsonConstructor]
        public CreateDiscount(Guid id, Guid customerId,
            string code, double percentage)
        {
            Id = id;
            CustomerId = customerId;
            Code = code;
            Percentage = percentage;
        }
    }
}
