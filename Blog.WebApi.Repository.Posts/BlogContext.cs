using Blog.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.WebApi.Repository.Posts
{
    public class BlogContext : DbContext
    {
        public DbSet<BlogPosts> Posts { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options)
         : base(options)
        {
            //irá criar o banco e a estrutura de tabelas necessárias
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<BlogPosts>(new PostConfiguration());
        }
    }
}

