namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class ProductCategories : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(nameof(ProductCategory), "Production");
            builder.HasKey(e => e.ProductCategoryId);
            builder.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            builder.Property(e => e.Name).IsRequired();

        }
    }
}
