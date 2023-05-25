using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using ThesisApi.Models;

namespace ThesisApi.Core.Context
{
    public class EvServiceContext : DbContext
    {
        public string DbPath { get; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PointModel> Points { get; set; }

        public EvServiceContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "evstations.db");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(x => x.Id);
                
            });
            modelBuilder.Entity<PointModel>(entity => { 
                entity.HasKey(x => x.Id); 
            });
            base.OnModelCreating(modelBuilder);

        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite($"Data Source=evstations.db");
    }
}
