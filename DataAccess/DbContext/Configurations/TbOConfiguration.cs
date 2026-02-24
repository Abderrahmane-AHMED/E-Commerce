using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mediateur.Models;

namespace Mediateur.Data.Configurations
{
    public class TbOConfiguration : IEntityTypeConfiguration<TbO>
    {
        public void Configure(EntityTypeBuilder<TbO> builder)
        {
            builder.HasKey(e => e.OsId);

            builder.Property(e => e.OsName)
                .HasMaxLength(100);
        }
    }
}
