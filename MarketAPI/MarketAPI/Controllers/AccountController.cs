using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Data;
using MarketAPI.Extensions;
using MarketAPI.Interfaces;
using MarketAPI.Models.Identity;
using MarketAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//TODO: Confirm email, reset and forgot password
namespace MarketAPI.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ICollectionRepository _productsRepository;

        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            AppDbContext context,
            ICollectionRepository productsRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _productsRepository = productsRepository;
        }

        //TODO: add auth notification
        [HttpPost, Route("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody]SignInViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (model != null && ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                            return Ok();
                        }
                    }
                    /*
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, details.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogInformation("User idiot.");
                        return RedirectToPage("./Lockout");
                    }
                    */
                }
            }

            ModelState.AddModelError("Login", "Invalid login attempt.");

            return BadRequest();
        }

        [HttpGet, Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Remove("productsList");
            await _signInManager.SignOutAsync();
            return Ok();
        }

        //TODO: confirmation reg. by email with SendGrid
        [HttpPost, Route("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody]SignUpViewModel model)
        {
            if(ModelState.IsValid && model != null && model.Password == model.ConfirmPassword)
            {
                var user = new AppUser(model);
                user.UserName = user.Surname + user.Name;

                var result = await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    var cartProducts = HttpContext.Session.Get<List<CartProducts>>("productsList");

                    user.Cart.Products = cartProducts;
                    await _userManager.UpdateAsync(user);
                    HttpContext.Session.Remove("productsList");

                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    return Ok();
                }
            }

            ModelState.AddModelError(string.Empty, "Unsuccessful registration attempt");

            return BadRequest();
        }
    }
}
