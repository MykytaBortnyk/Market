using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAPI.Data;
using MarketAPI.Extensions;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using MarketAPI.Models.Furniture;
using MarketAPI.Models.Identity;
using MarketAPI.Services;
using MarketAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Product> _repository;
        private readonly ICollectionRepository _productsRepository;

        public CartController(AppDbContext context,
            UserManager<AppUser> userManager,
            ICollectionRepository productsRepository,
            IRepository<Product> repository)
        {
            _context = context;
            _userManager = userManager;
            _productsRepository = productsRepository;
            _repository = repository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var cartId = (await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == _userManager.GetUserId(User))).Id;

                if (cartId != null)
                {
                    return Ok(_context.CartProducts.Where(c => c.Id == cartId));
                }

                return NotFound();
            }

            var cart = HttpContext.Session.Get<List<CartProducts>>("productsList");

            return Ok(cart);
        }

        // PUT api/values/5
        [HttpPut("{itemId}")]
        public async Task<IActionResult> Put(Guid itemId)
        {
            var product = await _repository.GetAsync(itemId);

            if (product != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var cartId = (await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == _userManager.GetUserId(User))).Id;

                    if (product != null && cartId != null)
                    {
                        await _productsRepository.AddAsync(cartId, product);
                        return Ok();
                    }
                }

                var cart = HttpContext.Session.Get<List<CartProducts>>("productsList");

                if (cart == null)
                {
                    cart = new List<CartProducts>();
                }

                var cartProduct = cart.FirstOrDefault(i => i.Product.Id == product.Id);

                if (cartProduct == null)
                {
                    cart.Add(new CartProducts { Cart = new Cart(), Product = product, Count = 1 });
                }
                else
                {
                    cartProduct.Count++;
                }

                HttpContext.Session.Remove("productsList");
                HttpContext.Session.Set("productsList", cart);
                return Ok(cart);
            }
            return BadRequest();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(Guid itemId)
        {
            if (itemId != Guid.Empty)
            {
                var product = await _repository.GetAsync(itemId);

                var cartId = (await _context.Carts.FirstOrDefaultAsync(c => c.AppUserId == _userManager.GetUserId(User))).Id;

                if (product != null && cartId != null)
                {
                    await _productsRepository.DeleteAsync(cartId, product);
                }
            }
            return NoContent();
        }
    }
}