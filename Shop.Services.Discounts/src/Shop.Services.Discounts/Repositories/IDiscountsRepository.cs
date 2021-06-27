using System.Threading.Tasks;
using Shop.Services.Discounts.Domain;

namespace Shop.Services.Discounts.Repositories
{
    public interface IDiscountsRepository
    {
        Task AddAsync(Discount discount);
        Task UpdateAsync(Discount discount);
    }
}