using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models.Identity
{
    public class Order : BaseEntity
    {
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public List<CartProducts> Products { get; set; }
    }
}
