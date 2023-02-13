using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EVEOnline.Data.Models;

public partial class MyEveonlineDbContext : DbContext
{
    public MyEveonlineDbContext()
    {
    }

    public MyEveonlineDbContext(DbContextOptions<MyEveonlineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbMarketOrder> TbMarketOrders { get; set; }

    public virtual DbSet<TbUniverseConstellation> TbUniverseConstellations { get; set; }

    public virtual DbSet<TbUniverseRace> TbUniverseRaces { get; set; }

    public virtual DbSet<TbUniverseRegion> TbUniverseRegions { get; set; }

    public virtual DbSet<TbUniverseStation> TbUniverseStations { get; set; }

    public virtual DbSet<TbUniverseSystem> TbUniverseSystems { get; set; }

    public virtual DbSet<TbUniverseType> TbUniverseTypes { get; set; }

    public virtual DbSet<TbUniverseTypeB> TbUniverseTypeBs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyEVEOnlineDB;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbMarketOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbMarket__3214EC074D7BAB8C");

            entity.ToTable("tbMarketOrder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Issued).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(22, 2)");
            entity.Property(e => e.Range).HasMaxLength(20);

            entity.HasOne(d => d.Location).WithMany(p => p.TbMarketOrders)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbMarketO__Locat__29221CFB");

            entity.HasOne(d => d.System).WithMany(p => p.TbMarketOrders)
                .HasForeignKey(d => d.SystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbMarketO__Syste__2A164134");

            entity.HasOne(d => d.Type).WithMany(p => p.TbMarketOrders)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbMarketO__TypeI__2B0A656D");
        });

        modelBuilder.Entity<TbUniverseConstellation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUniver__3214EC07AB971378");

            entity.ToTable("tbUniverseConstellation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(35);
            entity.Property(e => e.PositionX).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionY).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionZ).HasColumnType("decimal(22, 1)");

            entity.HasOne(d => d.Region).WithMany(p => p.TbUniverseConstellations)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbUnivers__Regio__76969D2E");
        });

        modelBuilder.Entity<TbUniverseRace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUniver__3214EC07BEB853E3");

            entity.ToTable("tbUniverseRace");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(40);
            entity.Property(e => e.NameRu).HasMaxLength(40);
        });

        modelBuilder.Entity<TbUniverseRegion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUniver__3214EC078F79C2F3");

            entity.ToTable("tbUniverseRegion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(35);
        });

        modelBuilder.Entity<TbUniverseStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUniver__3214EC07EE480412");

            entity.ToTable("tbUniverseStation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.PositionX).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionY).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionZ).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.ReprocessingEfficiency).HasColumnType("decimal(5, 4)");
            entity.Property(e => e.ReprocessingStationsTake).HasColumnType("decimal(5, 4)");
            entity.Property(e => e.Services).HasMaxLength(400);

            entity.HasOne(d => d.Race).WithMany(p => p.TbUniverseStations)
                .HasForeignKey(d => d.RaceId)
                .HasConstraintName("FK__tbUnivers__RaceI__245D67DE");

            entity.HasOne(d => d.System).WithMany(p => p.TbUniverseStations)
                .HasForeignKey(d => d.SystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbUnivers__Syste__25518C17");

            entity.HasOne(d => d.Type).WithMany(p => p.TbUniverseStations)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbUnivers__TypeI__2645B050");
        });

        modelBuilder.Entity<TbUniverseSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUniver__3214EC07AB100F27");

            entity.ToTable("tbUniverseSystem");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(35);
            entity.Property(e => e.PositionX).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionY).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionZ).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.SecurityClass).HasMaxLength(3);
            entity.Property(e => e.SecurityStatus).HasColumnType("decimal(20, 17)");

            entity.HasOne(d => d.Constellation).WithMany(p => p.TbUniverseSystems)
                .HasForeignKey(d => d.ConstellationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbUnivers__Const__797309D9");
        });

        modelBuilder.Entity<TbUniverseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUniver__3214EC076DFD7A9C");

            entity.ToTable("tbUniverseType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DogmaAttributes).HasMaxLength(400);
            entity.Property(e => e.DogmaEffects).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TbUniverseTypeB>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUniver__3214EC075D5CB6F7");

            entity.ToTable("tbUniverseTypeBS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DogmaAttributes).HasMaxLength(400);
            entity.Property(e => e.DogmaEffects).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
