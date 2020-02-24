using System.ComponentModel.DataAnnotations;
using MarketAPI.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MarketAPI.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser(SignUpViewModel model)
        {
            Name = model.Name;
            Surname = model.Surname;
            ZipCode = model.ZipCode;
            Address = model.Address;
            PhoneNumber = model.PhoneNumber;
            Cart = new Cart();
            Order = new Order();
            WishList = new WishList();
        }

        public AppUser() : base()
        { }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        public string Address { get; set; }

        public Cart Cart { get; set; }

        public Order Order { get; set; }

        public WishList WishList { get; set; }
    }
}