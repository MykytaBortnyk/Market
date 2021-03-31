using System.Collections.Generic;
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
            UserName = model.Surname + model.Name;
            Email = model.Email;
            PostalCode = model.PostalCode;
            Address = model.Address;
            PhoneNumber = model.PhoneNumber;
            Cart = new Cart();
        }

        public AppUser() : base()
        { }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string Address { get; set; }

        public Cart Cart { get; set; }
    }
}