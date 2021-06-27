using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Discounts.Domain;
using Shop.Services.Discounts.Messages.Commands;
using Shop.Services.Discounts.Messages.Events;
using Shop.Services.Discounts.Repositories;
using Microsoft.Extensions.Logging;

namespace Shop.Services.Discounts.Handlers.Discounts
{
    public class CreateDiscountHandler : ICommandHandler<CreateDiscount>
    {
        private readonly IDiscountsRepository _discountsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateDiscountHandler> _logger;

        public CreateDiscountHandler(IDiscountsRepository discountsRepository,
            ICustomersRepository customersRepository,
            IBusPublisher busPublisher, ILogger<CreateDiscountHandler> logger)
        {
            _discountsRepository = discountsRepository;
            _customersRepository = customersRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }


        public async Task HandleAsync(CreateDiscount command, ICorrelationContext context)
        {
            var customer = await _customersRepository.GetAsync(command.CustomerId);
            if(customer == null)
            {
                _logger.LogWarning($"Customer not found : {command.Id} was not found.");
                 return;
            }

            var discountwithoutCustomer = new Discount(command.Id, command.CustomerId,
                    command.Code, command.Percentage);
            await _discountsRepository.AddAsync(discountwithoutCustomer);
            await _busPublisher.PublishAsync(new DiscountCreated(command.Id,
            command.CustomerId, command.Code, command.Percentage), context);

        }
    }
}