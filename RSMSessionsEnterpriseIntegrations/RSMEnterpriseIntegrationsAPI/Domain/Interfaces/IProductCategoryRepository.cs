namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IProductCategoryRepository
    {

        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<ProductCategory?> GetProductCategoryById(int id);
        Task<int> CreateProductCategory(ProductCategory productCategory);
        Task<int> UpdateProductCategory(ProductCategory productCategory);
        Task<int> DeleteProductCategory(ProductCategory productCategory);

    }
}
