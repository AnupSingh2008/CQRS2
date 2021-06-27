using System;
using Shop.Common.Types;

namespace Shop.Services.Discounts.Domain
{
    public class Customer : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }

        private Customer()
        {

        }

        public Customer(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}