using System;

namespace Shop.Api.Models.Identity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}