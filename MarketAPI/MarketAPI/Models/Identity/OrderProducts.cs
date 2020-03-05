using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Models.Identity
{
    public class OrderProducts : BaseCollectionEntity
    {
        [ForeignKey("Id")]
        public Cart Cart { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
