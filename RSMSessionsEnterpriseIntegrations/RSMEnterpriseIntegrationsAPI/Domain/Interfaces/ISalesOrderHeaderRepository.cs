namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface ISalesOrderHeaderRepository
    {
        Task<IEnumerable<SalesOrderHeader>> GetSalesOrderHeaders();
        Task<SalesOrderHeader?> GetSalesOrderHeader(int id);
        Task<int> CreateSalesOrderHeader(SalesOrderHeader salesOrderHeader);
        Task<int> UpdateSalesOrderHeader(SalesOrderHeader salesOrderHeader);
        Task<int> DeleteSalesOrderHeader(SalesOrderHeader salesOrderHeader);
    }
}
