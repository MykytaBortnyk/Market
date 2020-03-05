using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAPI.Data;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using MarketAPI.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Services
{
    public class CollectionRepository<T> : ICollectionRepository<T> where T : BaseCollectionEntity, new()
    {
        private readonly AppDbContext _context;

        public CollectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetCartProductsAsync(Guid collectionId)
        {
            return await _context.Set<T>().Where(p => p.Id == collectionId).ToListAsync();
        }

        public async Task AddAsync(Guid collectionId, Product product)
        {
            var cart = await GetCartProductsAsync(collectionId);

            var cartProduct = cart.FirstOrDefault(i => i.Id == product.Id);

            if(cartProduct != null)
            {
                cartProduct.Count++;
                _context.Update(cartProduct);
            }
            else
            {
                _context.Set<T>().Add(new T { Id = collectionId, ProductId = product.Id, Count = 1 });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid collectionId, Product product)
        {
            var cart = await GetCartProductsAsync(collectionId);

            var cartProduct = cart.FirstOrDefault(i => i.Id == product.Id);

            if (cartProduct != null)
            {
                _context.Set<T>().Remove(cartProduct);
            }

            await _context.SaveChangesAsync();
        }
    }
}
