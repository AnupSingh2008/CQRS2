using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Customers.Messages.Commands;
using Shop.Services.Customers.Messages.Events;
using Shop.Services.Customers.Domain;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Customers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICartsRepository _cartsRepository;
        private readonly ICustomersRepository _customersRepository;

        public CreateCustomerHandler(IBusPublisher busPublisher,
            ICartsRepository cartsRepository,
            ICustomersRepository customersRepository)
        {
            _busPublisher = busPublisher;
            _cartsRepository = cartsRepository;
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(CreateCustomer command, ICorrelationContext context)
        {
          
            Customer obj = new Customer(command.Id, command.Email);
            obj.Complete(command.FirstName, command.LastName, command.Address, command.Country);
            await _customersRepository.UpdateAsync(obj);
            var cart = new Cart(command.Id);
            await _cartsRepository.AddAsync(cart);
            await _busPublisher.PublishAsync(new CustomerCreated(command.Id, obj.Email,
                command.FirstName, command.LastName, command.Address, command.Country), context);
        }
    }
}