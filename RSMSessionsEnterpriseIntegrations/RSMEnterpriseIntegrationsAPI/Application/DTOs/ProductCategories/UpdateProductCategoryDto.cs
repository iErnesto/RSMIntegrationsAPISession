namespace RSMEnterpriseIntegrationsAPI.Application.DTOs
{
    public class UpdateProductCategoryDto
    {
        public short ProductCategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid Rowguid { get; set; }
    }
}
