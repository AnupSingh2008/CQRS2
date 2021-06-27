using System;
using System.Threading.Tasks;
using Shop.Common.Mongo;
using Shop.Services.Identity.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Shop.Services.Identity.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _repository.GetAsync(x => x.Token == token);

        public async Task AddAsync(RefreshToken token)
            => await _repository.AddAsync(token);

        public async Task UpdateAsync(RefreshToken token)
            => await _repository.UpdateAsync(token);
    }
}