using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_SQLSERVER.Entities.Products;

namespace WPF_SQLSERVER.Infraestructure
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            try
            {
                var dbCreation = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreation == null) return;
                if (!dbCreation.CanConnect()) dbCreation.Create();
                if(!dbCreation.HasTables()) dbCreation.CreateTables();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(table =>
            {
                table.HasKey(p => p.Id);
                table.Property(p => p.Name).IsRequired();
                table.HasIndex(p => p.Name).IsUnique();
                table.Property(p => p.Description).IsRequired();
                table.Property(p => p.Price).IsRequired();
                table.Property(p => p.Stock).IsRequired();
            });
        }
    }
}
