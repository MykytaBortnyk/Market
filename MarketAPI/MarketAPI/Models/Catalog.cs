using System;
using System.Collections.Generic;

namespace MarketAPI.Models
{
    public class Catalog
    {
        public ICollection<Product> Products { get; set; }
    }
}
