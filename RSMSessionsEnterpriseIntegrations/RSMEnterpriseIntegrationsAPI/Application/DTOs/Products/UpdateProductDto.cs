namespace RSMEnterpriseIntegrationsAPI.Application.DTOs
{
    public class UpdateProductDto
    {
        public short ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal ListPrice { get; set; } = decimal.Zero;
    }
}
