using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlanovaniProjektu.Models;

public partial class DbProjektyContext : DbContext
{
    public DbProjektyContext()
    {
    }

    public DbProjektyContext(DbContextOptions<DbProjektyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbProjekt> TbProjekts { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbUloha> TbUlohas { get; set; }
    public virtual DbSet<TbUzivatel> TbUzivatels { get; set; }

    public virtual DbSet<TbUzivateleUlohy> TbUzivateleUlohies { get; set; }
    public virtual DbSet<TbAutorizacniTokeny> TbAutorizacniTokenies { get; set; }
    public virtual DbSet<TbProjektyUzivatele2> TbProjektyUzivateles { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-AU2L0VD;Database=db_projekty;User Id=sa;Password=123; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbProjekt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_Proje__3214EC07A39F4B10");

            entity.ToTable("tb_Projekt");

            entity.Property(e => e.DatumZacatku).HasColumnType("datetime");
            entity.Property(e => e.Nazev)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PlanovaneDokonceni).HasColumnType("datetime");
            entity.Property(e => e.Popis).IsUnicode(false);
            entity.Property(e => e.Stav)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.VedouciNavigation).WithMany(p => p.TbProjekts)
                .HasForeignKey(d => d.Vedouci)
                .HasConstraintName("FK__tb_Projek__Vedou__30F848ED");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_Role__3214EC07F8717AFF");

            entity.ToTable("tb_Role");

            entity.Property(e => e.Nazev)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbProjektyUzivatele2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_Proje__3214EC07D854F88F");

            entity.ToTable("tb_ProjektyUzivatele2");
        });

        modelBuilder.Entity<TbUloha>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_Uloha__3214EC07415EDAD1");

            entity.ToTable("tb_Uloha");

            entity.Property(e => e.Nazev)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Popis).IsUnicode(false);
        });

        modelBuilder.Entity<TbUzivatel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_Uziva__3214EC078E333AF2");

            entity.ToTable("tb_Uzivatel");

            entity.Property(e => e.BankovniSpojeni)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Heslo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Jmeno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mesto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrihlasovaciJmeno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prijmeni)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Psc)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("PSC");
            entity.Property(e => e.Telefon)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UliceCp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UliceCP");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.TbUzivatels)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK__tb_Uzivate__Role__2A4B4B5E");
        });

        modelBuilder.Entity<TbUzivateleUlohy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tb_Uziva__3214EC07494736E9");

            entity.ToTable("tb_UzivateleUlohy");

            entity.HasOne(d => d.UlohaNavigation).WithMany(p => p.TbUzivateleUlohies)
                .HasForeignKey(d => d.Uloha)
                .HasConstraintName("FK__tb_Uzivat__Uloha__3E52440B");

            entity.HasOne(d => d.UzivatelNavigation).WithMany(p => p.TbUzivateleUlohies)
                .HasForeignKey(d => d.Uzivatel)
                .HasConstraintName("FK__tb_Uzivat__Uziva__3D5E1FD2");
        });

        modelBuilder.Entity<TbAutorizacniTokeny>(entity =>
        {
            entity.ToTable("tb_AutorizacniTokeny");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Uzivatel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DatumVystaveni)
                .HasColumnType("datetime");
            entity.Property(e => e.DatumPlatnosti)
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
