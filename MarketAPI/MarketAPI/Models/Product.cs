using System;
using System.Collections;

namespace MarketAPI.Models
{
    public class Product : BaseEntity//, IAggregateRoot
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public int ItemsLeft { get; set; } 

        public string ImageUri { get; set; }

        //public ICollection
    }
    public class SomeTable : Product
    {
        public int SomePropOfTable { get; set; }
    }

    public class SomeChair : Product
    {
        public string SomePropOfChair { get; set; }
    }

    public interface IAggregateRoot
    { }
}
