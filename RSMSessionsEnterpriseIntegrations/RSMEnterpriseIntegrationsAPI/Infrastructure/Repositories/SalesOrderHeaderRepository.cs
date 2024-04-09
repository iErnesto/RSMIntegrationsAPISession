namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SalesOrderHeaderRepository : ISalesOrderHeaderRepository
    {
        private readonly AdvWorksDbContext _context;
        public SalesOrderHeaderRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesOrderHeader>> GetSalesOrderHeaders()
        {

            return await _context.Set<SalesOrderHeader>()
             .AsNoTracking()
             .ToListAsync();

        }

        public async Task<SalesOrderHeader?> GetSalesOrderHeader(int id)
        {
            return await _context.SalesOrderHeaders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SalesOrderId == id);
        }

        public async Task<int> CreateSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            await _context.AddAsync(salesOrderHeader);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            _context.Update(salesOrderHeader);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            _context.Remove(salesOrderHeader);
            return await _context.SaveChangesAsync();
        }

    }
}
