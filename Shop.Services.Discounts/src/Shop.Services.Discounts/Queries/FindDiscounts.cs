using System;
using System.Collections.Generic;
using Shop.Common.Types;
using Shop.Services.Discounts.Dto;

namespace Shop.Services.Discounts.Queries
{
    public class FindDiscounts : IQuery<IEnumerable<DiscountDto>>
    {
        public Guid CustomerId { get; set; }
    }
}