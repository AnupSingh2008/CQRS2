﻿using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Customers.Messages.Events
{
    [MessageNamespace("products")]
    public class ProductCreated : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Vendor { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public ProductCreated(Guid id, string name,
            string description, string vendor,
            decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Vendor = vendor;
            Price = price;
            Quantity = quantity;
        }
    }
}
