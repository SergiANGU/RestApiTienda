using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestApiTienda.Models;

public partial class TiendaRopaContext : DbContext
{
    public TiendaRopaContext()
    {
    }

    public TiendaRopaContext(DbContextOptions<TiendaRopaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_1");

            entity.ToTable("Producto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio");
            entity.Property(e => e.Talla)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("talla");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
