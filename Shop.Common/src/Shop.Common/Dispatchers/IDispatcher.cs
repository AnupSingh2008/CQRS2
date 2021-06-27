using System.Threading.Tasks;
using Shop.Common.Types;
using Shop.Common.Messages;

namespace Shop.Common.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}