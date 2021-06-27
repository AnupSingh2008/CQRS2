using Shop.Common.Types;
using Shop.Services.Products.Domain;
using Shop.Services.Products.Queries;
using System;
using System.Threading.Tasks;

namespace Shop.Services.Products.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<PagedResult<Product>> BrowseAsync(BrowseProducts query);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}