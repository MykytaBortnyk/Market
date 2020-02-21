using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketAPI.Data;
using MarketAPI.Interfaces;
using MarketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Services
{
    public class ProductsRepository : IProductsRepository<Product>
    {
        private readonly CatalogContext _catalogContext;

        public ProductsRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _catalogContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                _catalogContext.Products.Remove(product);
            }
        }

        public bool DoesItemExist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
