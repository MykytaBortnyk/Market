using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAPI.Data;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using MarketAPI.Models.Furniture;
using MarketAPI.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketAPI.Extensions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    public class MainPageController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly ICollectionRepository _productsRepository;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MainPageController(IRepository<Product> repository, ICollectionRepository productsRepository, AppDbContext context, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _productsRepository = productsRepository;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
           if (id != Guid.Empty)
            {
                var item = await _repository.GetAsync(id);

                if (item != null)
                {
                    return Ok(item);
                }
            }
            return NotFound();
        }
    }
}