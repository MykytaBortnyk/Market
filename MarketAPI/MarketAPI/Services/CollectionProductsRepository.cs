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
    public class CollectionProductsRepository : ICollectionRepository//<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public CollectionProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartProducts>> GetCartProductsAsync(Guid collectionId)
        {
            return await _context.CartProducts.Where(p => p.Id == collectionId).ToListAsync();
        }

        public async Task AddAsync(Guid collectionId, Product product, int? count)
        {
            var cart = await GetCartProductsAsync(collectionId);

            var cartProduct = cart.FirstOrDefault(i => i.Product == product);

            if(cartProduct != null)
            {
                cartProduct.Count++; //TODO: аутизм, надо переделать на + count
                _context.Update(cartProduct);
            }
            else
            {
                _context.CartProducts.Add(new CartProducts { Id = collectionId, Product = product, Count = count??= 0 });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid collectionId, Product product)
        {
            var cart = await GetCartProductsAsync(collectionId);

            var cartProduct = cart.FirstOrDefault(i => i.Product == product);

            if (cartProduct != null)
            {
                _context.CartProducts.Remove(cartProduct);
            }

            await _context.SaveChangesAsync();
        }
    }
}
