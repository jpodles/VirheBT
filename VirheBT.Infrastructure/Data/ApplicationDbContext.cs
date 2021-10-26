using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueComment> IssueComments { get; set; }
        public DbSet<IssueHistory> IssueHistories { get; set; }
        public DbSet<Project> Projects { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "ApplicationUsers");
                entity.Property(e => e.Id).HasColumnName("UserId");
            });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Projects)
                .WithMany(x => x.ApplicationUsers);

            modelBuilder.Entity<IssueComment>()
                .HasOne(e => e.Issue)
                .WithMany(e => e.IssueComments)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IssueHistory>()
                .HasOne(e => e.Issue)
                .WithMany(e => e.IssueHistory)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Maintainer)
                .WithMany(p => p.ProjectsMaintained)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Project>().HasData(
                 new Project
                 {
                     ProjectId = 1,
                     Name = "TestProject",
                     Description = "This is test project",
                     Created = DateTime.Now,
                     Status = Shared.Enums.ProjectStatus.OnTrack,
                 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}