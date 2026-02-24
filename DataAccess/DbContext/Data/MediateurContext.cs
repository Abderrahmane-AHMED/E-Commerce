using System;
using System.Collections.Generic;
using System.Runtime;
using Mediateur.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Mediateur.Interfaces;
using Mediateur.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace DataAccess.DbContext.Data;

public partial class MediateurContext : IdentityDbContext<ApplicationUser>
{
    public MediateurContext()
    {
    }
    public MediateurContext(DbContextOptions<MediateurContext> options)
        : base(options)
    {
    }
    public virtual DbSet<TbCategory> TbCategories { get; set; } = null!;
    public virtual DbSet<TbItem> TbItems { get; set; } = null!;
    public virtual DbSet<TbItemImage> TbItemImages { get; set; } = null!;
    public virtual DbSet<TbItemType> TbItemTypes { get; set; } = null!;
    public virtual DbSet<TbO> TbOs { get; set; } = null!;
    public virtual DbSet<TbPages> TbPages { get; set; }
    public virtual DbSet<TbSetting> TbSettings { get; set; } = null!;
    public virtual DbSet<VwItem> VwItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TbCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TbItemConfiguration());
        modelBuilder.ApplyConfiguration(new TbItemImageConfiguration());
        modelBuilder.ApplyConfiguration(new TbItemTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TbOConfiguration());
        modelBuilder.ApplyConfiguration(new TbPagesConfiguration());
        modelBuilder.ApplyConfiguration(new VwItemConfiguration());

    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

