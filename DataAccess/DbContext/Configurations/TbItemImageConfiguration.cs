using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mediateur.Models;

namespace Mediateur.Data.Configurations
{
    public class TbItemImageConfiguration : IEntityTypeConfiguration<TbItemImage>
    {
        public void Configure(EntityTypeBuilder<TbItemImage> builder)
        {
            builder.HasKey(e => e.ImageId);

            builder.Property(e => e.ImageName)
                .HasMaxLength(200);

            builder.HasOne(d => d.Item)
                .WithMany(p => p.TbItemImages)
                .HasForeignKey(d => d.ItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_TbItemImages_TbItems");
        }
    }
}
