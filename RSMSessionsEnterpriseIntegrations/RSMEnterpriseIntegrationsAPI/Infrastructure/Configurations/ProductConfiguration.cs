namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), "Production");

            builder.HasKey(e => e.ProductId);
            builder.Property(e => e.ProductId).HasColumnName("ProductID");
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Color).IsRequired();
            builder.Property(e => e.ListPrice).IsRequired();
        }
    }
}
