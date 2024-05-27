﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Models;

namespace MyRazorPagesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Answer> Answers { get; set; } = default!;
        public DbSet<View> Views { get; set; } = default!;
        public DbSet<Topic> Topics { get; set; } = default!;
        public DbSet<PostTopic> PostTopics { get; set; } = default!;

        public DbSet<Vote> Votes { get; set; } = default!;
        public DbSet<Community> Communities { get; set; } = default!;

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<PostTopic>()
        //         .HasOne(pt => pt.Post)
        //         .WithMany(p => p.PostTopics)
        //         .HasForeignKey(pt => pt.PostID);

        //     modelBuilder.Entity<PostTopic>()
        //         .HasOne(pt => pt.Topic)
        //         .WithMany(t => t.PostTopics)
        //         .HasForeignKey(pt => pt.TopicID);
        // }


    }
}