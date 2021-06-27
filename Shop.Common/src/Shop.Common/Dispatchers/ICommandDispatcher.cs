using System.Threading.Tasks;
using Shop.Common.Messages;

namespace Shop.Common.Dispatchers
{
    public interface ICommandDispatcher
    {
         Task SendAsync<T>(T command) where T : ICommand;
    }
}