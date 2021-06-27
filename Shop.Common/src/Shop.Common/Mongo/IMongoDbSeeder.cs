using System.Threading.Tasks;

namespace Shop.Common.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}