using System.Linq;
using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.Types;
using Shop.Services.Customers.Dto;
using Shop.Services.Customers.Queries;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Customers
{
    public class BrowseCustomersHandler : IQueryHandler<BrowseCustomers, PagedResult<CustomerDto>>
    {
        private readonly ICustomersRepository _customersRepository;

        public BrowseCustomersHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<PagedResult<CustomerDto>> HandleAsync(BrowseCustomers query)
        {
            var pagedResult = await _customersRepository.BrowseAsync(query);
            var customers = pagedResult.Items.Select(c => new CustomerDto
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Country = c.Country,
                CreatedAt = c.CreatedAt,
                Completed = c.Completed
            });

            return PagedResult<CustomerDto>.From(pagedResult, customers);
        }
    }
}