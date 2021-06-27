using System;
using Shop.Common.Messages;
using Newtonsoft.Json;

namespace Shop.Services.Identity.Messages.Events
{
    public class RefreshTokenRevoked : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public RefreshTokenRevoked(Guid userId)
        {
            UserId = userId;
        }
    }
}