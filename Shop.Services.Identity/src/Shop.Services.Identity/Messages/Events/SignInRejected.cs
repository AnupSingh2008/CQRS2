using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Identity.Messages.Events
{
    public class SignInRejected : IRejectedEvent
    {
        public string Email { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public SignInRejected(string email, string reason, string code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }
    }
}