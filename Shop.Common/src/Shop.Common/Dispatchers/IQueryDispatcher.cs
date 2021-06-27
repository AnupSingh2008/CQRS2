using System.Threading.Tasks;
using Shop.Common.Types;

namespace Shop.Common.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}