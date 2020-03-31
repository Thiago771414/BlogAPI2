using Blog.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.WebApi.Repository.Posts
{
    internal class PostConfiguration : IEntityTypeConfiguration<BlogPosts>
    {
        public void Configure(EntityTypeBuilder<BlogPosts> builder)
        {
            builder
                .Property(l => l.Titulo)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(l => l.Subtitulo)
                .HasColumnType("nvarchar(75)");

            builder
                .Property(l => l.Post)
                .HasColumnType("nvarchar(500)");

            builder
                .Property(l => l.Autor)
                .HasColumnType("nvarchar(75)");

            builder
                .Property(l => l.ImagemCapa);

            builder
                .Property(l => l.Lista)
                .HasColumnType("nvarchar(10)")
                .IsRequired()
                .HasConversion<string>(
                t => t.ParaString(),
                t => t.ParaTipo()
                );
        }
    }
}
