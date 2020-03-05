using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models.Identity
{
    public class CartProducts : BaseCollectionEntity
    {
        [ForeignKey("Id")]
        public Cart Cart { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public int Count { get; set; }
    }
}