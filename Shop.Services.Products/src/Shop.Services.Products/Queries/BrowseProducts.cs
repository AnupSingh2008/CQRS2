using Shop.Common.Types;
using Shop.Services.Products.Dto;

namespace Shop.Services.Products.Queries
{
    public class BrowseProducts : PagedQueryBase, IQuery<PagedResult<ProductDto>>
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; } = decimal.MaxValue;
    }
}
