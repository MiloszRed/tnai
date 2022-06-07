using System.Collections.Generic;
using System.Threading.Tasks;
using TNAI.Model.Entities;

namespace TNAI.Respository.Abstract
{
    public interface IProductRepository
    {
        Task<Product>       GetProductAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<bool>          SaveProductAsync(Product product);
        Task<bool>          DeleteProductAsync(int id);
    }
}
