﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using VirheBT.Data.Models;
using VirheBT.Pages;

namespace VirheBT.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueComment> IssueComments { get; set; }
        public DbSet<IssueHistory> IssueHistories { get; set; }
        public DbSet<Project> Projects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueHistory>()
                .HasOne(e => e.Issue)
                .WithMany(e => e.IssueHistory)
                .OnDelete(DeleteBehavior.Restrict);




            base.OnModelCreating(modelBuilder);
        }

    }
}
