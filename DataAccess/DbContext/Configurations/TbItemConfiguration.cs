using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mediateur.Models;

namespace Mediateur.Data.Configurations
{
    public class TbItemConfiguration : IEntityTypeConfiguration<TbItem>
    {
        public void Configure(EntityTypeBuilder<TbItem> builder)
        {
            builder.HasKey(e => e.ItemId);

            builder.HasIndex(e => e.ItemTypeId);
            builder.HasIndex(e => e.OsId);

            builder.Property(e => e.ItemName).HasMaxLength(100);
            builder.Property(e => e.ImageName).HasMaxLength(200);
            builder.Property(e => e.ItemTypeId).HasDefaultValueSql("((0))");
            builder.Property(e => e.OsId).HasDefaultValueSql("((0))");
            builder.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 2)");
            builder.Property(e => e.SalesPrice).HasColumnType("decimal(8, 2)");

            builder.HasOne(d => d.Category)
                .WithMany(p => p.TbItems)
                .HasForeignKey(d => d.CategoryId)
                 .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_TbItems_TbCategories");


            builder.HasOne(d => d.ItemType)
                .WithMany(p => p.TbItems)
                .HasForeignKey(d => d.ItemTypeId)
                  .HasConstraintName("FK_TbItems_TbItemTypes");
           

            builder.HasOne(d => d.Os)
                    .WithMany(p => p.TbItems)
                    .HasForeignKey(d => d.OsId)
                    .HasConstraintName("FK_TbItems_TbOs");


            builder.HasOne(d => d.Os)
                .WithMany(p => p.TbItems)
                .HasForeignKey(d => d.OsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
