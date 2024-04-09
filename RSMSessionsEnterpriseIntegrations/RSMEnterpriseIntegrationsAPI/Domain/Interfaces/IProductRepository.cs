namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IProductRepository
    {
     
        Task<IEnumerable<Product>> GetProducts();

    }
}
