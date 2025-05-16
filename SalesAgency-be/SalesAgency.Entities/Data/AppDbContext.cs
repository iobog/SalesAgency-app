using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalesAgency.Entities.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TClient> TClients { get; set; }

    public virtual DbSet<TOrder> TOrders { get; set; }

    public virtual DbSet<TOrderProduct> TOrderProducts { get; set; }

    public virtual DbSet<TProduct> TProducts { get; set; }

    public virtual DbSet<TUser> TUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TClient__3214EC07037F3CCC");

            entity.ToTable("TClient");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TOrder__3214EC07FC36048D");

            entity.ToTable("TOrder");

            entity.Property(e => e.Adress).HasMaxLength(250);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Client).WithMany(p => p.TOrders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__TOrder__ClientId__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.TOrders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TOrder__UserId__5165187F");
        });

        modelBuilder.Entity<TOrderProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TOrderPr__3214EC07BB8F5714");

            entity.ToTable("TOrderProduct");

            entity.HasOne(d => d.Order).WithMany(p => p.TOrderProducts)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__TOrderPro__Order__5535A963");

            entity.HasOne(d => d.Product).WithMany(p => p.TOrderProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__TOrderPro__Produ__5629CD9C");
        });

        modelBuilder.Entity<TProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TProduct__3214EC07E53ADC3B");

            entity.ToTable("TProduct");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Stock).HasDefaultValue(0);
        });

        modelBuilder.Entity<TUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TUser__3214EC075B818BFA");

            entity.ToTable("TUser");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
