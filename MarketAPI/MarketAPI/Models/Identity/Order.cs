using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models.Identity
{
    public class Order : BaseEntity
    {
        [Required]
        public string AspNetUsersId { get; set; }

        [ForeignKey("AspNetUsersId")]
        public AppUser AppUser { get; set; }

        public ICollection<Product> OrderItems { get; set; }
    }
}
