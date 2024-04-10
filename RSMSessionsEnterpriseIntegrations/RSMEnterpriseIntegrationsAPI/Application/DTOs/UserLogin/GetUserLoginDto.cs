namespace RSMEnterpriseIntegrationsAPI.Application.DTOs
{
    public class GetUserLoginDto
    {
        public required string Username { get; set; }
        public string? Role { get; set; }
    }
}
