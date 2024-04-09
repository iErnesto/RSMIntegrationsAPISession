namespace RSMEnterpriseIntegrationsAPI.Domain.Models

{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Production.Product")]
    public class Product

    {
        [Key]
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public decimal ListPrice { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
