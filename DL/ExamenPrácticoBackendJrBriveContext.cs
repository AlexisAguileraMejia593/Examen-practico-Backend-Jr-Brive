using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class ExamenPrácticoBackendJrBriveContext : DbContext
{
    public ExamenPrácticoBackendJrBriveContext()
    {
    }

    public ExamenPrácticoBackendJrBriveContext(DbContextOptions<ExamenPrácticoBackendJrBriveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacoras { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-CHR9HTDV; Database=Examen práctico Backend Jr Brive; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.IdBitacora).HasName("PK__Bitacora__ED3A1B13D6686E68");

            entity.ToTable("Bitacora");

            entity.HasOne(d => d.IdProductosNavigation).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.IdProductos)
                .HasConstraintName("FK__Bitacora__IdProd__267ABA7A");

            entity.HasOne(d => d.IdUsuariosNavigation).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.IdUsuarios)
                .HasConstraintName("FK__Bitacora__IdUsua__25869641");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProductos).HasName("PK__Producto__718C7D07B929E489");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sdk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SDK");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK__Productos__IdSuc__1273C1CD");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__BFB6CD99092A14E7");

            entity.ToTable("Sucursal");

            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PK__Usuarios__EAEBAC8F263869E6");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
