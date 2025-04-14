using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(u => u.CustomerId).HasColumnType("uuid").IsRequired();
            builder.Property(u => u.BranchId).HasColumnType("uuid").IsRequired();

            builder.Property(u => u.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");


            builder.Property(u => u.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

        }
    }
}