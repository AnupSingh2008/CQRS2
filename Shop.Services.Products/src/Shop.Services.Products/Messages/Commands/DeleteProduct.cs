using Shop.Common.Messages;
using Newtonsoft.Json;
using System;

namespace Shop.Services.Products.Messages.Commands
{
	public class DeleteProduct : ICommand
	{
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteProduct(Guid id)
        {
            Id = id;
        }
	}
}