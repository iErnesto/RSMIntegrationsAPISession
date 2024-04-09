namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;

    public interface IProductCategoryService
    {
        Task<IEnumerable<GetProductCategoryDto>> GetAll();
        Task<GetProductCategoryDto?> GetProductCategoryById (int id);
        Task<int> CreateProductCategory(CreateProductCategoryDto createProductCategoryDto);
        Task<int> UpdateProductCategory(UpdateProductCategoryDto updateProductCategoryDto);
        Task<int> DeleteProductCategory(int id);

    }
}
