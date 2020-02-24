using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Models;

namespace MarketAPI.Interfaces
{
    public interface IRepository<T> where T : BaseEntity//, IAggregateRoot
    {
        Task<bool> DoesItemExist(Guid id);
        IEnumerable<T> Get();
        Task<IEnumerable<T>> GetAsync();
        T Get(Guid id);
        Task<T> GetAsync(Guid id);
        void Create(T item);
        Task CreateAsync(T item);
        void Update(T item);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}