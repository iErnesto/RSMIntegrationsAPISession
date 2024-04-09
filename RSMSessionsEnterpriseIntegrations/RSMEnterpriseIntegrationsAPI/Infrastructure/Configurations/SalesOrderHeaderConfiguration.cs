using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSMEnterpriseIntegrationsAPI.Domain.Models;

namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    public class SalesOrderHeaderConfiguration : IEntityTypeConfiguration<SalesOrderHeader>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeader> builder)
        {
            builder.ToTable(nameof(SalesOrderHeader), "Sales");

            builder.HasKey(e => e.SalesOrderId);
            builder.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            builder.Property(e => e.RevisionNumber).IsRequired();
            builder.Property(e => e.OrderDate).IsRequired();
            builder.Property(e => e.DueDate).IsRequired();
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.OnlineOrderFlag).IsRequired();
            builder.Property(e => e.SalesOrderNumber).IsRequired();
            builder.Property(e => e.BillToAddressId).IsRequired();
            builder.Property(e => e.ShipToAddressId).IsRequired();
            builder.Property(e => e.ShipMethodId).IsRequired();
            builder.Property(e => e.SubTotal).IsRequired();
            builder.Property(e => e.TaxAmt).IsRequired();
            builder.Property(e => e.Freight).IsRequired();
            builder.Property(e => e.TotalDue).IsRequired();
            builder.Property(e => e.Rowguid).IsRequired();
            builder.Property(e => e.ModifiedDate).IsRequired();
            
            
        }
    }
}
