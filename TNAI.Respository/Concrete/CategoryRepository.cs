using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

using TNAI.Model.Entities;
using TNAI.Respository.Abstract;

namespace TNAI.Respository.Concrete
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        /// <inheritdoc />
        public async Task<Category>       GetCategoryAsync(int id)
        {
            return await Context.Categories.SingleOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await Context.Categories.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool>           SaveCategoryAsync(Category category)
        {
            if (category == null)
                return false;

            try
            {
                Context.Entry(category).State = category.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public async Task<bool>           DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);
            if (category == null)
                return true;

            Context.Categories.Remove(category);

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
