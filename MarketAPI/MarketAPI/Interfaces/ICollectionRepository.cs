using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Models;
using MarketAPI.Models.Identity;

namespace MarketAPI.Interfaces
{
    public interface ICollectionRepository
    {
        public Task<List<CartProducts>> GetCartProductsAsync(Guid collectionId);
        public Task AddAsync(Guid collectionId, Product product, int? count);
        public Task DeleteAsync(Guid collectionId, Product product);
    }
}