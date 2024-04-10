namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs;

    public interface IUserLoginService
    {

        Task<IEnumerable<GetUserLoginDto>> GetAll();
        Task<GetUserLoginDto?> GetUserLoginById(int id);
        Task<int> CreateUserLogin(CreateUserLoginDto createUserLoginDto);
        Task<int> DeleteUserLogin(int id);
     
       
    }
}
