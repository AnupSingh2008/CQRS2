using Shop.Common.Handlers;
using Shop.Services.Products.Dto;
using Shop.Services.Products.Queries;
using Shop.Services.Products.Repositories;
using System.Threading.Tasks;

namespace Shop.Services.Products.Handlers
{
    public sealed class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;

        public async Task<ProductDto> HandleAsync(GetProduct query)
        {
            var product = await _productsRepository.GetAsync(query.Id);

            return product == null ? null : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price
            };
        }
    }
}
