namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProductById(int id);
        Task<int> CreateProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<int> DeleteProduct(Product product);

    }
}
