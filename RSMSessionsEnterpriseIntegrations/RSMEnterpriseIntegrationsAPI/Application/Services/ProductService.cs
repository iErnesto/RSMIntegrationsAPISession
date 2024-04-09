namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;
    using System.Collections.Generic;
    using System.Formats.Asn1;
    using System.Runtime.Intrinsics.Arm;
    using System.Threading.Tasks;

    public class ProductService(IProductRepository repository) : IProductService
    {
        private readonly IProductRepository _productRepository = repository;

        public async Task<IEnumerable<GetProductDto>> GetAll()
        {
            var products = await _productRepository.GetProducts();

            if (products == null)
            {
                return [];
            }

            List<GetProductDto> productDtos = [];

            foreach (var product in products)
            {
                GetProductDto dto = new GetProductDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Color = product.Color,
                    ListPrice = product.ListPrice
                };

                productDtos.Add(dto);
            }

            return productDtos;
        }

        public async Task<GetProductDto?> GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("ProductId is not valid");
            }

            var product = await ValidateProductExistence(id);

            GetProductDto dto = new()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Color = product.Color,
                ListPrice = product.ListPrice
            };
            return dto;
        }

        public async Task<int> CreateProduct(CreateProductDto createProductDto)
        {
            if (createProductDto is null
            || string.IsNullOrWhiteSpace(createProductDto.Name)
            || string.IsNullOrWhiteSpace(createProductDto.Color)
            || decimal.IsCanonical(createProductDto.ListPrice))
            {
                throw new BadRequestException("Product info is not valid");
            }

            Product product = new()
            {
                Name = createProductDto.Name,
                Color = createProductDto.Color,
                ListPrice = createProductDto.ListPrice
            };
            return await _productRepository.CreateProduct(product);

        }

        public async Task<int> UpdateProduct(UpdateProductDto updateProductDto)
        {
            if (updateProductDto is null)
            {
                throw new BadRequestException("Product info is not valid.");
            }

            var product = await ValidateProductExistence(updateProductDto.ProductID);

            product.Name = string.IsNullOrWhiteSpace(updateProductDto.Name) ? null : updateProductDto.Name;
            product.Color = string.IsNullOrWhiteSpace(updateProductDto.Color) ? null : updateProductDto.Color;
            product.ListPrice = decimal.IsEvenInteger(updateProductDto.ListPrice) ? 0 : updateProductDto.ListPrice;

            return await _productRepository.UpdateProduct(product);
        }

        public async Task<int> DeleteProduct(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException($"Unable to delete product with id {id}");
            }
            var product = await ValidateProductExistence(id);
            return await _productRepository.DeleteProduct(product);
        }

        private async Task<Product> ValidateProductExistence(int id)
        {
            var existingProduct = await _productRepository.GetProductById(id)
                ?? throw new NotFoundException($"Product with Id: {id} was not found.");

            return existingProduct;
        }


    }
}
