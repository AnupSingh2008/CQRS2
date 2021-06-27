using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Identity.Messages.Events
{
    public class AccessTokenRevoked : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public AccessTokenRevoked(Guid userId)
        {
            UserId = userId;
        }
    }
}