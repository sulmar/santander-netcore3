using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Domain;

namespace Shopper.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
               .Property(p => p.Id)
               .HasColumnName("ProductId");

            builder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
