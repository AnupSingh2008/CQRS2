using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Services.Customers.Dto;
using Shop.Services.Customers.Queries;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Customers
{
    public class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDto>
    {
        private readonly ICustomersRepository _customersRepository;

        public GetCustomerHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<CustomerDto> HandleAsync(GetCustomer query)
        {
            var customer = await _customersRepository.GetAsync(query.Id);

            return customer == null ? null : new CustomerDto
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Country = customer.Country,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}