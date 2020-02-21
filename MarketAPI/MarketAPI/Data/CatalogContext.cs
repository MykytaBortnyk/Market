using System;
using MarketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Data
{
    public class CatalogContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }
    }
}
