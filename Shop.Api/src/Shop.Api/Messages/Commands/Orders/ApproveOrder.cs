using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Api.Messages.Commands.Orders
{
    [MessageNamespace("orders")]
    public class ApproveOrder : ICommand
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ApproveOrder(Guid id)
        {
            Id = id;
        }
    }
}