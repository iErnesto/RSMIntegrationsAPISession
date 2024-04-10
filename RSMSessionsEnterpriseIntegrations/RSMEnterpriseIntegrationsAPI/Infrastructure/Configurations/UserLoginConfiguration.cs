using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSMEnterpriseIntegrationsAPI.Domain.Models;

namespace RSMEnterpriseIntegrationsAPI.Infrastructure.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable(nameof(UserLogin), "Person");

            builder.HasKey(t => t.UserId);
            builder.Property(t => t.UserId).ValueGeneratedOnAdd();
            builder.Property(t => t.Username).IsRequired();
            builder.Property(t => t.Password).IsRequired();
            builder.Property(t => t.Role).IsRequired();

        }
    }
}
