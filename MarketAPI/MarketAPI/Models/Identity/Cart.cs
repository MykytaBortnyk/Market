using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models.Identity
{
    public class Cart : BaseEntity
    {
        public Cart() {}
        public Cart(List<CartProducts> products)
        {
            Products = products;
        }

        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public List<CartProducts> Products { get; set; }
    }
}