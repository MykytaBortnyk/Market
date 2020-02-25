using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    public class MainPageController : Controller
    {
        private readonly IRepository<Product> _repository;

        public MainPageController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _repository.GetAsync());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                var item = _repository.Get(id);

                if(item != null)
                {
                    return Ok(item);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product value)
        {
            if(value != null)
            {
                return Ok(await _repository.CreateAsync(value));
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task PutAsync(Guid id, [FromBody]Product value)
        {
            if(id != Guid.Empty)
            {
                var product = await _repository.GetAsync(id);

                if(product != null)
                {
                    _repository.Update(value);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                var product = await _repository.GetAsync(id);

                if (product != null)
                {
                    await _repository.DeleteAsync(product);
                }
            }
        }
    }
}
