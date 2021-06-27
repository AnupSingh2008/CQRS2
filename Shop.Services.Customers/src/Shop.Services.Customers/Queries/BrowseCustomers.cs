using Shop.Common.Types;
using Shop.Services.Customers.Dto;

namespace Shop.Services.Customers.Queries
{
    public class BrowseCustomers : PagedQueryBase, IQuery<PagedResult<CustomerDto>>
    {
    }
}