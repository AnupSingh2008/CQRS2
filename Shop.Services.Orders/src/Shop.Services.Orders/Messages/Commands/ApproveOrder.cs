using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Orders.Messages.Commands
{
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