namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;

    public interface IProductService
    {

        Task<IEnumerable<GetProductDto>> GetAll();

    }
}
