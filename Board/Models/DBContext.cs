using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Board.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DBContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Importance> Importances { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SubPost> SubPosts { get; set; }
        public DbSet<PostFile> PostFiles { get; set; }
        public DbSet<SubPostFile> SubPostFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Post>()
                .HasMany(e => e.SubPost)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);
        }
    }
}