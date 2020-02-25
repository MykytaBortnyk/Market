using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketAPI.Data;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Services
{
    public class ProductsRepository<T> : IRepository<T> where T : Product
    {
        private readonly CatalogContext _catalogContext;

        public ProductsRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public void Delete(Guid id)
        {
            var product = Get(id);

            if (product != null) _catalogContext.Set<T>().Remove(product);

            _catalogContext.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await GetAsync(id);

            if (product != null) _catalogContext.Set<T>().Remove(product);

            await _catalogContext.SaveChangesAsync();
        }

        public async Task<bool> DoesItemExist(Guid id)
        {
            return await _catalogContext.Set<T>().AnyAsync(p => p.Id == id);
        }

        public T Get(Guid id)
        {
            return _catalogContext.Set<T>().Find(id);
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _catalogContext.Set<T>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public IEnumerable<T> Get()
        {
            return _catalogContext.Set<T>().ToList();
        }
        
        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _catalogContext.Set<T>().ToListAsync();
        }
        
        public void Create(T item)
        {
            _catalogContext.Set<T>().Add(item);
        }

        public async Task CreateAsync(T item)
        {
            await _catalogContext.Set<T>().AddAsync(item);

            await _catalogContext.SaveChangesAsync();
        }

        public void Update(T item)
        {
            _catalogContext.Entry(item).State = EntityState.Modified;

            _catalogContext.SaveChanges();
        }
    }
}