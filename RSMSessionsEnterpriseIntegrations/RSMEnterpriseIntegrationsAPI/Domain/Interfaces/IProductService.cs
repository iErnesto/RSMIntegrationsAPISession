namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;

    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAll();
        Task<GetProductDto?> GetProductById(int id);
        Task<int> CreateProduct(CreateProductDto productDtoDto);
        Task<int> UpdateProduct(UpdateProductDto productDto);
        Task<int> DeleteProduct(int id);

    }
}
