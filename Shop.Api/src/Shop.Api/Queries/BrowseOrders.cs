using System;

namespace Shop.Api.Queries
{
    public class BrowseOrders : PagedQuery
    {
        public Guid CustomerId { get; set; }
    }
}
