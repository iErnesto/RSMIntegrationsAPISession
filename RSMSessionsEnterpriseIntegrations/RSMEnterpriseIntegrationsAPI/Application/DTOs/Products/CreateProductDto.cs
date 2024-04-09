namespace RSMEnterpriseIntegrationsAPI.Application.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal ListPrice { get; set; } = decimal.Zero;
    }
}
