using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mediateur.Models;

namespace Mediateur.Data.Configurations
{
    public class VwItemConfiguration : IEntityTypeConfiguration<VwItem>
    {
        public void Configure(EntityTypeBuilder<VwItem> builder)
        {
            builder.HasNoKey();
            builder.ToView("VwItem");

            builder.Property(e => e.ItemName).HasMaxLength(100);
            builder.Property(e => e.CategoryName).HasMaxLength(100);
            builder.Property(e => e.ItemTypeName).HasMaxLength(100);
            builder.Property(e => e.ItemTypeName).HasMaxLength(100);
            builder.Property(e => e.OsName).HasMaxLength(100);
            builder.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            builder.Property(e => e.SalesPrice).HasColumnType("decimal(8,2)");
        }
    }
}
