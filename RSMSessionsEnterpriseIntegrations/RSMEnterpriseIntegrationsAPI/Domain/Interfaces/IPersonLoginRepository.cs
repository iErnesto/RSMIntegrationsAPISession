namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public interface IUserLoginRepository
    {

        Task<IEnumerable<UserLogin>> GetUserLogins();
        Task<UserLogin?> GetUserLogin(int id);
        Task<int> CreateUserLogin(UserLogin userLogin);
        Task<int> DeleteUserLogin(UserLogin userLogin);
        Task<UserLogin?> GetUserByUsername(string username);


    }
}
