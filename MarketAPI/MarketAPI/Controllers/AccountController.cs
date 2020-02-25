using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAPI.Models;
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


        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
            await _signInManager.SignOutAsync();
            return Ok();
        }

        //TODO: confirmation reg. by email with SendGrid
        [HttpPost, Route("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody]SignUpViewModel model)
        {
            if(ModelState.IsValid && model != null)
            {
                var user = new AppUser(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    return Ok();
                }
            }

            ModelState.AddModelError(string.Empty, "Unsuccessful registration attempt");

            return BadRequest();
        }
    }
}
