using System.Threading.Tasks;
using Shop.Common.Mongo;
using Shop.Services.Discounts.Domain;

namespace Shop.Services.Discounts.Repositories
{
    public class DiscountsRepository : IDiscountsRepository
    {
        private readonly IMongoRepository<Discount> _repository;

        public DiscountsRepository(IMongoRepository<Discount> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Discount discount)
            => await _repository.AddAsync(discount);

        public async Task UpdateAsync(Discount discount)
            => await _repository.UpdateAsync(discount);
    }
}