using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.Mongo;
using Shop.Services.Discounts.Domain;
using Shop.Services.Discounts.Dto;
using Shop.Services.Discounts.Queries;

namespace Shop.Services.Discounts.Handlers.Discounts
{
    public class GetDiscountHandler : IQueryHandler<GetDiscount, DiscountDetailsDto>
    {
        private readonly IMongoRepository<Discount> _discountsRepository;
        private readonly IMongoRepository<Customer> _customersRepository;

        public GetDiscountHandler(IMongoRepository<Discount> discountsRepository,
            IMongoRepository<Customer> customersRepository)
        {
            _discountsRepository = discountsRepository;
            _customersRepository = customersRepository;
        }
        
        public async Task<DiscountDetailsDto> HandleAsync(GetDiscount query)
        {
            var discount = await _discountsRepository.GetAsync(query.Id);
            if (discount is null)
            {
                return null;
            }

            var customer = await _customersRepository.GetAsync(discount.CustomerId);

            return new DiscountDetailsDto
            {
                Customer = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.Email
                },
                Discount = new DiscountDto
                {
                    Id = discount.Id,
                    CustomerId = discount.CustomerId,
                    Code = discount.Code,
                    Percentage = discount.Percentage,
                    Available = !discount.UsedAt.HasValue
                }
            };
        }
    }
}