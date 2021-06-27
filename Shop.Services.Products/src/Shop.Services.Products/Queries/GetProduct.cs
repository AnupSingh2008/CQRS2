using Shop.Common.Types;
using Shop.Services.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services.Products.Queries
{
    public class GetProduct : IQuery<ProductDto>
    {
        public Guid Id { get; set; }
    }
}
