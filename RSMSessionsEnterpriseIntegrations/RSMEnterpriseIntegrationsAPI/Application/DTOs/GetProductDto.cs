namespace RSMEnterpriseIntegrationsAPI.Application.DTOs
{
    public class GetProductDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public decimal? ListPrice { get; set; }
    }
}
