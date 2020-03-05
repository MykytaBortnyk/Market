using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarketAPI.Models.Identity;

namespace MarketAPI.Models
{
    public class ProductComment : BaseEntity
    {
        [Required]
        public string AspNetUsersId { get; set; }

        [ForeignKey("AspNetUsersId")]
        public AppUser AppUser { get; set; }

        public int Rate { get; set; }

        public string CommentText { get; set; }
    }
}