using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Services.Discounts.Domain;

namespace Shop.Services.Discounts.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetAsync(Guid id);
        Task AddAsync(Customer customer);
    }
}