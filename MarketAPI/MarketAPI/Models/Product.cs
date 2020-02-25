using System.Collections.Generic;

namespace MarketAPI.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Cost { get; set; }

        public int ItemsLeft { get; set; }

        public string ImageUri { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Depth { get; set; }

        public string Color { get; set; }

        public int CommentsCount { get; set; }

        public int Rate { get; set; }

        public string AdditionalCharacteristics { get; set; }

        public ICollection<string> Comments { get; set; }
    }
}
