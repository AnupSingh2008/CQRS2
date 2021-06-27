using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Identity.Messages.Events
{
    public class PasswordChanged : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public PasswordChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}