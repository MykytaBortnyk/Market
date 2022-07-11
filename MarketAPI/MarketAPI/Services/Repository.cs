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
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _catalogContext;

        public Repository(AppDbContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public void Delete(T item)
        {
            _catalogContext.Set<T>().Remove(item);

            _catalogContext.SaveChanges();
        }

        public async Task DeleteAsync(T item)
        {
            _catalogContext.Set<T>().Remove(item);

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
            return await _catalogContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> Get()
        {
            return _catalogContext.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var s = typeof(T);

            return await _catalogContext.Set<T>().ToListAsync();
        }

        public T Create(T item)
        {
            _catalogContext.Set<T>().Add(item);

            _catalogContext.SaveChanges();

            return item;
        }

        public async Task<T> CreateAsync(T item)
        {
            await _catalogContext.Set<T>().AddAsync(item);

            await _catalogContext.SaveChangesAsync();

            return item;
        }

        public async Task UpdateAsync(T item)
        {
            _catalogContext.Update(item); //Or just update it

            await _catalogContext.SaveChangesAsync();
        }
    }
}