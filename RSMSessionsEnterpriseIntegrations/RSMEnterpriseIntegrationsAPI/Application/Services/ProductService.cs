namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task<IEnumerable<GetProductDto>> GetAll()
        {
            var products = await _productRepository.GetProducts();

            if (products == null)
            {
                return Enumerable.Empty<GetProductDto>(); // Return an empty collection
            }

            List<GetProductDto> productDtos = new List<GetProductDto>();

            foreach (var product in products)
            {
                GetProductDto dto = new GetProductDto
                {
                    Name = product.Name,
                    Color = product.Color,
                    ListPrice = product.ListPrice
                };

                productDtos.Add(dto);
            }

            return productDtos;
        }
    }
}
