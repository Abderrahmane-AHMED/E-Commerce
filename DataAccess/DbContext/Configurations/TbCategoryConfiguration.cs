using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mediateur.Models;

namespace Mediateur.Data.Configurations
{
    public class TbCategoryConfiguration : IEntityTypeConfiguration<TbCategory>
    {
        public void Configure(EntityTypeBuilder<TbCategory> builder)
        {

            builder.HasKey(e => e.CategoryId);

         

            builder.Property(e => e.CategoryName).HasMaxLength(50);
            builder.Property(e => e.CreatedBy).HasDefaultValueSql("(N'')");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

            builder.Property(e => e.ImageName).HasDefaultValueSql("(N'')");




        }
    }
}
