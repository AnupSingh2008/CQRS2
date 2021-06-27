using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Domain;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Identity
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly ICustomersRepository _customersRepository;

        public SignedUpHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(SignedUp @event, ICorrelationContext context)
        {
            var customer = new Customer(@event.UserId, @event.Email);
            await _customersRepository.AddAsync(customer);
        }
    }
}