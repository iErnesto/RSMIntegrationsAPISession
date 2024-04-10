using RSMEnterpriseIntegrationsAPI.Application.DTOs;
using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
using RSMEnterpriseIntegrationsAPI.Domain.Models;

namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    public class ProductCategoryService(IProductCategoryRepository repository) : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository = repository;

        public async Task<IEnumerable<GetProductCategoryDto>> GetAll()
        {
            var productCategories = await _productCategoryRepository.GetProductCategories();

            if (productCategories == null)
            {
                return [];
            }

            List<GetProductCategoryDto> productCategoryDtos = [];

            foreach (var productCategory in productCategories)
            {
                GetProductCategoryDto dto = new GetProductCategoryDto
                {
                    ProductCategoryId = productCategory.ProductCategoryId , 
                    Name = productCategory.Name,
                };

                productCategoryDtos.Add(dto);
            }

            return productCategoryDtos;
        }

        public async Task<GetProductCategoryDto?> GetProductCategoryById (int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("ProductCategoryId is not valid");
            }

            var productCategory = await ValidateProductCategoryExistence(id);

            GetProductCategoryDto dto = new()
            {
                  ProductCategoryId = productCategory.ProductCategoryId,
                  Name = productCategory.Name,
            };
            return dto;
        }

        public async Task<int> CreateProductCategory(CreateProductCategoryDto createProductCategoryDto)
        {
            if (createProductCategoryDto is null
            || string.IsNullOrWhiteSpace(createProductCategoryDto.Name))
         
            {
                throw new BadRequestException("ProductCategoryId info is not valid");
            }

            ProductCategory productCategory = new()
            {
                Name = createProductCategoryDto.Name,
               
            };
            return await _productCategoryRepository.CreateProductCategory(productCategory);

        }

        public async Task<int> UpdateProductCategory(UpdateProductCategoryDto updateProductCategoryDto)
        {
            if (updateProductCategoryDto is null)
            {
                throw new BadRequestException("ProductCategory info is not valid.");
            }

            var productCategory = await ValidateProductCategoryExistence(updateProductCategoryDto.ProductCategoryID );

          productCategory.Name = updateProductCategoryDto.Name;
          

            return await _productCategoryRepository.UpdateProductCategory(productCategory);
        }

        public async Task<int> DeleteProductCategory(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException($"Unable to delete productCategort with id {id}");
            }
            var productCategory = await ValidateProductCategoryExistence(id);
            return await _productCategoryRepository.DeleteProductCategory(productCategory);
        }

            private async Task<ProductCategory> ValidateProductCategoryExistence(int id)
        {
            var existingProductCategory = await _productCategoryRepository.GetProductCategoryById (id)
                ?? throw new NotFoundException($"ProductCategory with Id: {id} was not found.");

            return existingProductCategory;
        }


    }
}
