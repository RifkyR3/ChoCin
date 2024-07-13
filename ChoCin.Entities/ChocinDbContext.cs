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

    public virtual DbSet<CGroup> CGroups { get; set; }

    public virtual DbSet<CModule> CModules { get; set; }

    public virtual DbSet<CUser> CUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PRIMARY");

            entity.HasMany(d => d.Modules).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "CGroupModule",
                    r => r.HasOne<CModule>().WithMany()
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("c_group_module_ibfk_2"),
                    l => l.HasOne<CGroup>().WithMany()
                        .HasForeignKey("GroupId")
                        .HasConstraintName("c_group_module_ibfk_1"),
                    j =>
                    {
                        j.HasKey("GroupId", "ModuleId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("c_group_module");
                        j.HasIndex(new[] { "GroupId" }, "GroupId");
                        j.HasIndex(new[] { "ModuleId" }, "ModuleId");
                    });
        });

        modelBuilder.Entity<CModule>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PRIMARY");

            entity.HasOne(d => d.ModuleSub).WithMany(p => p.InverseModuleSub)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("c_module_ibfk_1");
        });

        modelBuilder.Entity<CUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.HasMany(d => d.Groups).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "CUserGroup",
                    r => r.HasOne<CGroup>().WithMany()
                        .HasForeignKey("GroupId")
                        .HasConstraintName("c_user_group_ibfk_4"),
                    l => l.HasOne<CUser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("c_user_group_ibfk_3"),
                    j =>
                    {
                        j.HasKey("UserId", "GroupId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("c_user_group");
                        j.HasIndex(new[] { "GroupId" }, "GroupId");
                        j.HasIndex(new[] { "UserId" }, "UserId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
