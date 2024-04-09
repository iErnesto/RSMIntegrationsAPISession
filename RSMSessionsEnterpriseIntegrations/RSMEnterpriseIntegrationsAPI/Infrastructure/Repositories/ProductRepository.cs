namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly AdvWorksDbContext _context;
        public ProductRepository(AdvWorksDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetProducts()
        {

               return await _context.Set<Product>()
                .AsNoTracking()
                .ToListAsync();

        }
    }
}
