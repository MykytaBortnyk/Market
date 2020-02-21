using System;
namespace MarketAPI.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public int ItemsLeft { get; set; } 

        public string ImageUri { get; set; }
    }
}
