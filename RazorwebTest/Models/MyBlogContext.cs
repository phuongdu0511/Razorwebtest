using System;
using Microsoft.EntityFrameworkCore;

namespace RazorwebTest.Models
{
    public class MyBlogContext : DbContext
    {
        public MyBlogContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Article> articles { get; set; }
    }
}
