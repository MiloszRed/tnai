using System.Collections.Generic;
using System.Threading.Tasks;

using TNAI.Model.Entities;

namespace TNAI.Respository.Abstract
{
    public interface ICategoryRepository
    {
        Task<Category>       GetCategoryAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<bool>           SaveCategoryAsync(Category category);
        Task<bool>           DeleteCategoryAsync(int id);
    }
}
