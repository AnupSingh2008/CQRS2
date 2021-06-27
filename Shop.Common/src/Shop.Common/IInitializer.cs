using System.Threading.Tasks;

namespace Shop.Common
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}