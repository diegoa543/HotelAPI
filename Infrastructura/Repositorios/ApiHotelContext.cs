using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HOTEL_API.Infrastructura.Repositorios;

public partial class DbHotelContext : DbContext
{
    public DbHotelContext()
    {
    }

    public DbHotelContext(DbContextOptions<DbHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<HabitacionReserva> HabitacionReservas { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Huesped> Huespeds { get; set; }

    public virtual DbSet<HuespedReserva> HuespedReservas { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosPerfil> UsuariosPerfils { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-B1EMHD1; Database=API_HOTEL; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ciudad__3213E83F001744E2");

            entity.ToTable("Ciudad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaisId).HasColumnName("Pais_id");

            entity.HasOne(d => d.Pais).WithMany(p => p.Ciudads)
                .HasForeignKey(d => d.PaisId)
                .HasConstraintName("FK__Ciudad__Pais_id__37A5467C");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habitaci__3213E83FF3F3F1B6");

            entity.ToTable("Habitacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HotelId).HasColumnName("Hotel_id");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Hotel).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Habitacio__Hotel__38996AB5");
        });

        modelBuilder.Entity<HabitacionReserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habitaci__159697704F559689");

            entity.ToTable("Habitacion_Reserva");

            entity.Property(e => e.HabitacionId).HasColumnName("Habitacion_id");
            entity.Property(e => e.ReservaId).HasColumnName("Reserva_id");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.HabitacionReservas)
                .HasForeignKey(d => d.HabitacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__Habit__3A81B327");

            entity.HasOne(d => d.Reserva).WithMany(p => p.HabitacionReservas)
                .HasForeignKey(d => d.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__Reser__3B75D760");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hotel__3213E83FE8DB6D17");

            entity.ToTable("Hotel");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CiudadId).HasColumnName("Ciudad_id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.CiudadId)
                .HasConstraintName("FK__Hotel__Ciudad_id__3B75D760");
        });

        modelBuilder.Entity<Huesped>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Huesped__3213E83FA06847D3");

            entity.ToTable("Huesped");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Documento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TelefonoMovil)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocu)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HuespedReserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Huesped___7F5545847E58523B");

            entity.ToTable("Huesped_Reserva");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HuespedId).HasColumnName("Huesped_id");
            entity.Property(e => e.ReservaId).HasColumnName("Reserva_id");

            entity.HasOne(d => d.Huesped).WithMany(p => p.HuespedReservas)
                .HasForeignKey(d => d.HuespedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huesped_R__Huesp__3E52440B");

            entity.HasOne(d => d.Reserva).WithMany(p => p.HuespedReservas)
                .HasForeignKey(d => d.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huesped_R__Reser__3F466844");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pais__3213E83F9D13AC77");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__Perfil__1D1C87683879C889");

            entity.ToTable("Perfil");

            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.NombrePerfil)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Nombre_perfil");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reserva__3213E83FD8DB4157");

            entity.ToTable("Reserva");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Reserva__Usuario__3E52440B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04AD67AD582F");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contra)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("contra");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsuariosPerfil>(entity =>
        {
            entity.ToTable("Usuarios_Perfil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.UsuariosPerfils)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("FK__Usuarios___id_pe__34C8D9D1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuariosPerfils)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Usuarios___id_us__33D4B598");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
