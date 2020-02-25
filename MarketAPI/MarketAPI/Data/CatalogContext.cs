using System;
using System.Collections.Generic;
using MarketAPI.Models.Furniture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarketAPI.Data
{
    public class CatalogContext : DbContext
    {
        public DbSet<OfficeTable> OfficeTables { get; set; }

        public DbSet<Chair> Chairs { get; set; }

        public DbSet<Сupboard> Сupboards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        public static void CreateBaseProducts(IServiceProvider serviceProvider)
        {
            var _context = serviceProvider.GetRequiredService<CatalogContext>();
            _context.Chairs.Add(new Chair
            {
                Title = "Designer's chair",
                Description = "One chair for four designers",
                Category = "Ultimate chair",
                Cost = 1925.3,//3132.78
                ItemsLeft = 1,
                //ImageUri = new Uri(string.Format()),
                Width = 480,
                Height = 860,
                Depth = 480,
                Color = "Sky Blue",
                CommentsCount = 0,
                Comments = new List<string>(),
                Rate = 0,
                AdditionalCharacteristics = string.Empty,
                MaxWeight = 400,
                BodyMaterial = "Steel",
                UpholsteryMaterial = "Leatherette",
                Body = "Aluminium",
                Upholstery = "Vinyl leather",
                SeatSize = 480,
                Basetype = "Conference base"
            });
            _context.SaveChanges();
        }
    }
}
