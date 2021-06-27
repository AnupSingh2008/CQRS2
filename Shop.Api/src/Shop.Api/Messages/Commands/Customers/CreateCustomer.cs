using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Api.Messages.Commands.Customers
{
    [MessageNamespace("customers")]
    public class CreateCustomer : ICommand
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Country { get; }

        public string Email { get; set; }

        [JsonConstructor]
        public CreateCustomer(Guid id, string firstName, string lastName, 
            string address, string country,string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Country = country;
            Email = email;
        }        
    }
}