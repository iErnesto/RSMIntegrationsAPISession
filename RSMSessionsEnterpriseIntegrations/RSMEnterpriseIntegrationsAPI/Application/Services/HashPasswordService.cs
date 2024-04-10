namespace RSMEnterpriseIntegrationsAPI.Application.Services

{

    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
