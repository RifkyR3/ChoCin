using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Entities;

public partial class ChocinDbContext : DbContext
{
    public ChocinDbContext(DbContextOptions<ChocinDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CUser> CUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
