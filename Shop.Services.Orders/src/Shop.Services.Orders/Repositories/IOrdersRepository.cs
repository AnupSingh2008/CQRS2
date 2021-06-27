using Shop.Common.Types;
using Shop.Services.Orders.Domain;
using Shop.Services.Orders.Queries;
using System;
using System.Threading.Tasks;

namespace Shop.Services.Orders.Repositories
{
    public interface IOrdersRepository
    {
        Task<bool> HasPendingOrder(Guid customerId);
        Task<Order> GetAsync(Guid id);
        Task<PagedResult<Order>> BrowseAsync(BrowseOrders query);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}
