using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Models;

namespace RedeSocial.Infra.Data.Context {
    public class RedeSocialContext : DbContext {
        public RedeSocialContext(DbContextOptions<RedeSocialContext> options)
                : base(options) {
        }
        public DbSet<Users>? Users { get; set; }
        public DbSet<Tags>? Tags { get; set; }
        public DbSet<PostsPhotos>? PostsPhotos { get; set; }
        public DbSet<Posts>? Posts { get; set; }
        public DbSet<Likes>? Likes { get; set; }
        public DbSet<Followers>? Followers { get; set; }
        public DbSet<Comments>? Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

    }
}
