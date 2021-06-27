using System;
using System.Threading.Tasks;
using Shop.Common.Mongo;
using Shop.Services.Customers.Domain;

namespace Shop.Services.Customers.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IMongoRepository<Product> _repository;
        
        public ProductsRepository(IMongoRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Product product)
            => await _repository.AddAsync(product);

        public async Task UpdateAsync(Product product)
            => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}