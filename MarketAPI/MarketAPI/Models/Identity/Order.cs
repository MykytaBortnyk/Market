using System;
using System.Collections;
using System.Collections.Generic;

namespace MarketAPI.Models.Identity
{
    public class Order : BaseEntity
    {
        public ICollection<Product> OrderItems { get; set; }
    }
}
