namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserLoginRepository(AdvWorksDbContext context) : IUserLoginRepository
    {
        private readonly AdvWorksDbContext _context = context;

        public async Task<IEnumerable<UserLogin>> GetUserLogins()
        {

            return await _context.Set<UserLogin>()
             .AsNoTracking()
             .ToListAsync();

        }

        public async Task<UserLogin?> GetUserLogin(int id)
        {
            return await _context.UserLogins
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<int> CreateUserLogin(UserLogin userLogin)
        {
            await _context.AddAsync(userLogin);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUserLogin(UserLogin userLogin)
        {
            _context.Remove(userLogin);

            return await _context.SaveChangesAsync();
        }

        public async Task<UserLogin?> GetUserByUsername(string username)
        {
            return await _context.UserLogins.FirstOrDefaultAsync(u => u.Username == username);
        }


    }
}
