using System;
using Shop.Common.Types;
using Shop.Services.Discounts.Dto;

namespace Shop.Services.Discounts.Queries
{
    public class GetDiscount : IQuery<DiscountDetailsDto>
    {
        public Guid Id { get; set; } 
    }
}