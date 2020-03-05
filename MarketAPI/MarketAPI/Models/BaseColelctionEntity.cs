using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models
{
    public class BaseCollectionEntity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Count { get; set; }
    }
}
