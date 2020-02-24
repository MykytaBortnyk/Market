using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using MarketAPI.Models.Identity;

namespace MarketAPI.Services
{
    public class CartRepository : ICartRepository
    {
        public CartRepository()
        {
        }

        public void Create(Cart item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Cart item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesItemExist(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cart> Get()
        {
            throw new NotImplementedException();
        }

        public Cart Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cart>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Cart item)
        {
            throw new NotImplementedException();
        }
    }
}
