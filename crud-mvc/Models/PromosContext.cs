using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Models;

public partial class PromosContext : DbContext
{
    public PromosContext()
    {
    }

    public PromosContext(DbContextOptions<PromosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cupone> Cupones { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //      => optionsBuilder.UseSqlServer(ConfigurationManager.ConnetionStrings["conexion"].ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clientes__3214EC07F91424B5");

            entity.ToTable("clientes");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Cupone>(entity =>
        {
            entity.HasKey(e => e.IdCupon).HasName("PK__cupones__F98CB48CC1F257AD");

            entity.ToTable("cupones");

            entity.Property(e => e.IdCupon).HasColumnName("Id_cupon");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.CodigoCupon)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("codigo_cupon");
            entity.Property(e => e.Credito)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("credito");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_vencimiento");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cupones)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cupones__cliente__35BCFE0A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
