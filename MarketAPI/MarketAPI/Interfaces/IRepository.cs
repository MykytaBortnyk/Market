using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Models;

namespace MarketAPI.Interfaces
{
    public interface IChairRepository<T> where T : BaseEntity//, IAggregateRoot
    {
        Task<bool> DoesItemExist(Guid id);
        IEnumerable<T> Get();
        Task<IEnumerable<T>> GetAsync();
        T Get(Guid id);
        Task<T> GetAsync(Guid id);
        T Create(T item);
        Task<T> CreateAsync(T item);
        Task UpdateAsync(T item);
        void Delete(T item);
        Task DeleteAsync(T item);
    }
}