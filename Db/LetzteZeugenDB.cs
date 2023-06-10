using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Letzte_Zeugen.Db;

public partial class LetzteZeugenDB : DbContext
{
    public LetzteZeugenDB()
    {
    }

    public LetzteZeugenDB(DbContextOptions<LetzteZeugenDB> options)
        : base(options)
    {
    }

    public virtual DbSet<Bautypus> Bautypus { get; set; }

    public virtual DbSet<BeteiligtePersonen> BeteiligtePersonen { get; set; }

    public virtual DbSet<Eigentuemer> Eigentuemer { get; set; }

    public virtual DbSet<Existent> Existent { get; set; }

    public virtual DbSet<Gefaehrdung> Gefaehrdung { get; set; }

    public virtual DbSet<Institute> Institute { get; set; }

    public virtual DbSet<Link> Link { get; set; }

    public virtual DbSet<Material> Material { get; set; }

    public virtual DbSet<Messmodell> Messmodell { get; set; }

    public virtual DbSet<Modell> Modell { get; set; }

    public virtual DbSet<Ort> Ort { get; set; }

    public virtual DbSet<Person> Person { get; set; }

    public virtual DbSet<Projekt> Projekt { get; set; }

    public virtual DbSet<Realisiert> Realisiert { get; set; }

    public virtual DbSet<Zustand> Zustand { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bautypus>(entity =>
        {
            entity.Property(e => e.Bautypus1).HasColumnName("Bautypus");
        });

        modelBuilder.Entity<BeteiligtePersonen>(entity =>
        {
            entity.HasKey(e => new { e.IDModell, e.IDPerson });
        });

        modelBuilder.Entity<Existent>(entity =>
        {
            entity.HasIndex(e => e.ID, "IX_Existent_ID").IsUnique();

            entity.Property(e => e.Existent1).HasColumnName("Existent");
        });

        modelBuilder.Entity<Gefaehrdung>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Gefaehrdung1).HasColumnName("Gefaehrdung");
        });

        modelBuilder.Entity<Institute>(entity =>
        {
            entity.HasOne(d => d.KoordinateNavigation).WithMany(p => p.Institute).HasForeignKey(d => d.Koordinate);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.Property(e => e.Material1).HasColumnName("Material");
        });

        modelBuilder.Entity<Messmodell>(entity =>
        {
            entity.HasIndex(e => e.ID, "IX_Messmodell_ID").IsUnique();

            entity.Property(e => e.Messmodell1).HasColumnName("Messmodell");
        });

        modelBuilder.Entity<Projekt>(entity =>
        {
            entity.Property(e => e.Projekt1).HasColumnName("Projekt");

            entity.HasOne(d => d.IDRealisiertNavigation).WithMany(p => p.Projekt).HasForeignKey(d => d.IDRealisiert);
        });

        modelBuilder.Entity<Realisiert>(entity =>
        {
            entity.HasIndex(e => e.ID, "IX_Realisiert_ID").IsUnique();

            entity.Property(e => e.Realisiert1).HasColumnName("Realisiert");
        });

        modelBuilder.Entity<Zustand>(entity =>
        {
            entity.HasIndex(e => e.ID, "IX_Zustand_ID").IsUnique();

            entity.Property(e => e.Zustand1).HasColumnName("Zustand");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
