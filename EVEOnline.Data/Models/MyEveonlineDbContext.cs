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

    public virtual DbSet<Uconstellation> Uconstellations { get; set; }

    public virtual DbSet<Uregion> Uregions { get; set; }

    public virtual DbSet<Usystem> Usystems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyEVEOnlineDB;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Uconstellation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UConstel__3214EC07F58035E0");

            entity.ToTable("UConstellation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(35);
            entity.Property(e => e.PositionX).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionY).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionZ).HasColumnType("decimal(22, 1)");

            entity.HasOne(d => d.Region).WithMany(p => p.Uconstellations)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UConstell__Regio__5EBF139D");
        });

        modelBuilder.Entity<Uregion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__URegions__3214EC0722D51ABA");

            entity.ToTable("URegions");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(35);
        });

        modelBuilder.Entity<Usystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USystem__3214EC07C25AB893");

            entity.ToTable("USystem");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(35);
            entity.Property(e => e.PositionX).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionY).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.PositionZ).HasColumnType("decimal(22, 1)");
            entity.Property(e => e.SecurityClass).HasMaxLength(3);
            entity.Property(e => e.SecurityStatus).HasColumnType("decimal(20, 17)");

            entity.HasOne(d => d.Constellation).WithMany(p => p.Usystems)
                .HasForeignKey(d => d.ConstellationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USystem__Constel__619B8048");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
