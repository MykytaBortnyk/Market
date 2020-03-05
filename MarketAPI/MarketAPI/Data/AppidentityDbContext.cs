using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Models;
using MarketAPI.Models.Furniture;
using MarketAPI.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartProducts> CartProducts { get; set; }

        public DbSet<Chair> Chairs { get; set; }

        public DbSet<Сupboard> Сupboards { get; set; }

        public DbSet<OfficeTable> OfficeTables { get; set; }

        public DbSet<ProductComment> ProductComments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartProducts>().HasKey(k => new { k.Id, k.ProductId });
            //modelBuilder.Entity<CartProducts>().HasOne(c => c.Cart).WithMany(cp => cp.Products);

            base.OnModelCreating(modelBuilder);
        }

        public static void CreateBaseProducts(IServiceProvider serviceProvider)
        {
            var _context = serviceProvider.GetRequiredService<AppDbContext>();//CatalogContext>();
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
                Comments = new List<ProductComment>(),
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

        public static async Task CreateBaseAccount(IServiceProvider serviceProvider)
        {
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _context = serviceProvider.GetRequiredService<AppDbContext>();//CatalogContext>();
            var userName = "Joe";
            var email = "Joe@example.com";
            var role = "Admin";
            var passvvord = "Pas$.Vvord";
            AppUser user = new AppUser
            {
                UserName = userName,
                Email = email,
                Name = "Joe",
                Surname = "Examplov",
                Cart = new Cart { Products = new List<CartProducts>() }
            };
            await _roleManager.CreateAsync(new IdentityRole(role));
            await _roleManager.CreateAsync(new IdentityRole("User"));
            await _userManager.CreateAsync(user, passvvord);
            await _userManager.AddToRoleAsync(user, role);
            await _context.SaveChangesAsync();
        }
    }
}
