using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopper.Domain.Models;

namespace Shopper.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
               .Property(p => p.Id)
               .HasColumnName("CustomerId");

            builder
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
