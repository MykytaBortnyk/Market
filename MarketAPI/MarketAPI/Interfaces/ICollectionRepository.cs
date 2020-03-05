using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Models;
using MarketAPI.Models.Identity;

namespace MarketAPI.Interfaces
{
    public interface ICollectionRepository<T> where T : BaseCollectionEntity, new()
    {
        public Task<List<T>> GetCartProductsAsync(Guid collectionId);
        public Task AddAsync(Guid collectionId, Product product);
        public Task DeleteAsync(Guid collectionId, Product product);
    }
}