using System;
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
        public virtual DbSet<Author_Tag> Author_Tag { get; set; }
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
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Author_Tag>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.TagId });

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Author_Tag)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Author_Tag_Author");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Author_Tag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Author_Tag_Tag");

                //entity.HasOne(x => x.PostView).WithMany(x => x.Author_Tag).HasForeignKey(x => x.AuthorId).HasPrincipalKey(x => x.AuthorId);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Author");
            });

            modelBuilder.Entity<PostView>(entity =>
            {
                entity.HasMany(d => d.Post_Tag)
                    .WithOne()
                    .HasForeignKey(x => x.PostId);
                
                entity.HasMany(d => d.Author_Tag)
                    .WithOne()
                    .HasForeignKey(x => x.AuthorId)
                    .HasPrincipalKey(x => x.AuthorId);
                /* without HasPrincipalKey it joins on PostView.PostId = AuthorId:
                   SELECT [p].[Id], [p].[AuthorId], [a].[AuthorId], [a].[TagId]
                   FROM [PostView] AS [p]
                   LEFT JOIN [Author_Tag] AS [a] ON [p].[Id] = [a].[AuthorId]

                   But *with* HasPrincipalKey it SELECTS a nonexisting column, AuthorId1
                   SELECT [p].[Id], [p].[AuthorId], [p].[AuthorId1], [a].[AuthorId], [a].[TagId]
                   FROM [PostView] AS [p]
                   LEFT JOIN [Author_Tag] AS [a] ON [p].[AuthorId] = [a].[AuthorId]
                   ORDER BY [p].[Id], [a].[AuthorId], [a].[TagId]
                 */

                entity.ToView("PostView");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Post_Tag>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.TagId });

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Post_Tag)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Tag_Post");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Post_Tag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Tag_Tag");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
