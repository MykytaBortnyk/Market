using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models.Identity
{
    public class Cart : BaseEntity
    {
        [Required]
        public string AspNetUsersId { get; set; }

        [ForeignKey("AspNetUsersId")]
        public AppUser AppUser { get; set; }

        public ICollection<Product> CartProducts { get; set; }

        public double TotalPrice { get; set; }
    }
}
