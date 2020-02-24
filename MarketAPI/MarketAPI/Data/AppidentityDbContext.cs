using System;
using MarketAPI.Models;
using MarketAPI.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Data
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Cart> Carts { get; set; }

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
