using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Services.Orders.Domain;
using Shop.Services.Orders.Messages.Events;
using Shop.Services.Orders.Repositories;
using Shop.Services.Orders.Services;
using System.Threading.Tasks;

namespace Shop.Services.Orders.Handlers.Customers
{
    public class CustomerCreatedHandler : IEventHandler<CustomerCreated>
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomerCreatedHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(CustomerCreated @event, ICorrelationContext context) =>
            await _customersRepository.AddAsync(new Customer(@event.Id, @event.Email,
                @event.FirstName, @event.LastName, @event.Address, @event.Country));
    }
}
