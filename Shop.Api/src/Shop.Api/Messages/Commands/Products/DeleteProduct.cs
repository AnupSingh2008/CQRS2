using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Api.Messages.Commands.Products
{
    [MessageNamespace("products")]
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