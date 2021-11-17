using Blog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class BlogDbContext : DbContext
    {
        // tek bir connection string'e bağlamamak için aşağıdaki yapıyı kullanıyoruz.
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });

            modelBuilder.Entity<Category>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Post>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Tag>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Comment>().Property(x => x.IsActive).HasDefaultValue(true);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}







