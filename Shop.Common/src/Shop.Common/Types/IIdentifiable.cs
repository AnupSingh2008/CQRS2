using System;

namespace Shop.Common.Types
{
    public interface IIdentifiable
    {
         Guid Id { get; }
    }
}