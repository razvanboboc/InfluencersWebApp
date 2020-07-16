using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Influencers.Models
{
    public partial class InfluencersContext : DbContext
    {
        public InfluencersContext()
        {
        }

        public InfluencersContext(DbContextOptions<InfluencersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleTags> ArticleTags { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-MKTUAUF\\SQLEXPRESS;Initial Catalog=Influencers;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.AddedTime).HasColumnType("datetime");

                entity.Property(e => e.AuthorId).HasColumnName("Author_Id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Article__Author___398D8EEE");
            });

            modelBuilder.Entity<ArticleTags>(entity =>
            {
                entity.HasKey(x => new { x.ArticleId, x.TagId });

                entity.HasIndex(e => new { e.ArticleId, e.TagId })
                    .HasName("UQ__ArticleT__7D0083163AC3ED2A")
                    .IsUnique();

                entity.Property(e => e.ArticleId).HasColumnName("Article_Id");

                entity.Property(e => e.TagId).HasColumnName("Tag_Id");

                entity.HasOne(d => d.Article)
                    .WithMany(t => t.Tags)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArticleTa__Artic__3E52440B");

                entity.HasOne(d => d.Tag)
                    .WithMany(a => a.Articles)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArticleTa__Tag_I__3F466844");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.AddedTime).HasColumnType("datetime");

                entity.Property(e => e.ArticleId).HasColumnName("Article_Id");

                entity.Property(e => e.AuthorId).HasColumnName("Author_Id");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK__Comment__Article__4D94879B");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Comment__Author___4CA06362");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
