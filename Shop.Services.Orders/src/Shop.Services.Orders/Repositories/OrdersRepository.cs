using System;
using System.Threading.Tasks;
using Shop.Common.Mongo;
using Shop.Common.Types;
using Shop.Services.Orders.Domain;
using Shop.Services.Orders.Queries;
using static Shop.Services.Orders.Domain.Order;

namespace Shop.Services.Orders.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoRepository<Order> _repository;

        public OrdersRepository(IMongoRepository<Order> repository)
            => _repository = repository;

        public async Task<bool> HasPendingOrder(Guid customerId)
            => await _repository.ExistsAsync(o => o.CustomerId == customerId &&
                                                  (o.Status == OrderStatus.Created ||
                                                   o.Status == OrderStatus.Approved));

        public async Task<Order> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<PagedResult<Order>> BrowseAsync(BrowseOrders query)
            => await _repository.BrowseAsync(o => o.CustomerId == query.CustomerId, query);

        public async Task AddAsync(Order order)
            => await _repository.AddAsync(order);

        public async Task UpdateAsync(Order order)
            => await _repository.UpdateAsync(order);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}
