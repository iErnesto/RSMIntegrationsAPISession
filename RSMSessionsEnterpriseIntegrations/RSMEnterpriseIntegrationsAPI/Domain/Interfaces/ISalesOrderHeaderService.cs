namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;

    public interface ISalesOrderHeaderService
    {
        Task<IEnumerable<GetSalesOrderHeaderDto>> GetAll();
        Task<GetSalesOrderHeaderDto?> GetSalesOrderHeaderById(int id);
        Task<int> CreateSalesOrderHeader(CreateSalesOrderHeaderDto createSalesOrderHeaderDto);
        Task<int> UpdateSalesOrderHeader(UpdateSalesOrderHeaderDto updateSalesOrderHeaderDto);
        Task<int> DeleteSalesOrderHeader(int id);

    }
}
