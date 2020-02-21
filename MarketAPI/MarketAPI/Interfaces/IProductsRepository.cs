using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketAPI.Interfaces
{
    public interface IProductsRepository<Product>
    {
        bool DoesItemExist(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> Get(Guid id);
        Task InsertAsync(Product item);
        Task UpdateAsync(Product item);
        Task DeleteAsync(Guid id);
    }
}
