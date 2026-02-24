using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mediateur.Models;

namespace Mediateur.Data.Configurations
{
    public class TbPagesConfiguration : IEntityTypeConfiguration<TbPages>
    {
        public void Configure(EntityTypeBuilder<TbPages> builder)
        {

            builder.HasKey(e => e.PageId);

            builder.Property(e => e.Title).HasMaxLength(500);


            builder.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");

        

            builder.Property(e => e.ImageName).HasDefaultValueSql("(N'')");




        }
    }
}
