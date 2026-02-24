using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mediateur.Models;

namespace Mediateur.Data.Configurations
{
    public class TbItemTypeConfiguration : IEntityTypeConfiguration<TbItemType>
    {
        public void Configure(EntityTypeBuilder<TbItemType> builder)
        {
            builder.HasKey(e => e.ItemTypeId);

            builder.Property(e => e.ItemTypeName)
                .HasMaxLength(100);
        }
    }
}
