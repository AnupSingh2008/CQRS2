using System;
using System.Threading.Tasks;
using Shop.Common.Types;
using Shop.Services.Customers.Domain;
using Shop.Services.Customers.Queries;

namespace Shop.Services.Customers.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetAsync(Guid id);
        Task<PagedResult<Customer>> BrowseAsync(BrowseCustomers id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}