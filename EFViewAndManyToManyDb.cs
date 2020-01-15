﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFViewAndManyToMany
{
    public partial class EFViewAndManyToManyDb : DbContext
    {
        public EFViewAndManyToManyDb()
        {
        }

        public EFViewAndManyToManyDb(DbContextOptions<EFViewAndManyToManyDb> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostView> PostView { get; set; }
        public virtual DbSet<Post_Tag> Post_Tag { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=EFViewAndManyToMany;User Id=localsa;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Author");
            });

            modelBuilder.Entity<PostView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PostView");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Post_Tag>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.TagId });

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Post_Tag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Tag_Tag");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}