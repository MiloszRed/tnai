using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

using TNAI.Model.Entities;
using TNAI.Respository.Abstract;

namespace TNAI.Respository.Concrete
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        /// <inheritdoc />
        public async Task<Product>       GetProductAsync(int id)
        {
            return await Context.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await Context.Products.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool>          SaveProductAsync(Product product)
        {
            if (product == null)
                return false;

            try
            {
                Context.Entry(product).State = product.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public async Task<bool>          DeleteProductAsync(int id)
        {
            var product = await GetProductAsync(id);
            if (product == null)
                return true;

            Context.Products.Remove(product);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
