using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ParkContext : DbContext
    {
        private string _connectionString;
        public ParkContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<ParkEF> Parken { get; set; }
        public DbSet<HuisEF> Huizen { get; set; }
        public DbSet<HuurderEF> Huurders { get; set; }
        public DbSet<HuurcontractEF> Huurcontracten { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString)
              .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkEF>()
                .HasMany(p => p.Huizen)
                .WithOne(h => h.Park)
                .HasForeignKey(h => h.ParkId);
            modelBuilder.Entity<HuurderEF>()
                .HasMany(h => h.Huurcontracten)
                .WithOne(hc => hc.Huurder)
                .HasForeignKey(hc => hc.HuurderId);
            modelBuilder.Entity<HuisEF>()
                .HasMany(h => h.Huurcontracten)
                .WithOne(h => h.Huis)
                .HasForeignKey(h => h.HuisId);
        }
    }
}
