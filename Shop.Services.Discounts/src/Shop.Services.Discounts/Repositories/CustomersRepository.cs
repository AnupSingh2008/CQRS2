using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shop.Common.Mongo;
using Shop.Services.Discounts.Domain;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Shop.Services.Discounts.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IMongoRepository<Customer> _repository;

        public CustomersRepository(IMongoRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            //return await _repository.GetAsync(id);

            BsonDefaults.GuidRepresentation = GuidRepresentation.PythonLegacy;

            var collection = new MongoClient().GetDatabase("discounts-service").GetCollection<Customer>("Customers");

            var item = collection.Find(x => x.Id == id)
            .FirstOrDefault();

            return item;
        }

        public Task AddAsync(Customer customer)
            => _repository.AddAsync(customer);
    }
}