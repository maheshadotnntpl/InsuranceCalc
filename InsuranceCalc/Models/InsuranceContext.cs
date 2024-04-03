using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCalc.Models;

public partial class InsuranceContext : DbContext
{
    public InsuranceContext()
    {
    }

    public InsuranceContext(DbContextOptions<InsuranceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Occupation> Occupations { get; set; }

    public virtual DbSet<OccupationRating> OccupationRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Occupati__3214EC0785A1773E");

            entity.ToTable("Occupation");

            entity.Property(e => e.OccupationName).HasMaxLength(255);

            entity.HasOne(d => d.RatingNavigation).WithMany(p => p.Occupations)
                .HasForeignKey(d => d.Rating)
                .HasConstraintName("FK__Occupatio__Ratin__3A81B327");
        });

        modelBuilder.Entity<OccupationRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Occupati__3214EC0737FD3662");

            entity.ToTable("OccupationRating");

            entity.Property(e => e.Factor).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Rating).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
