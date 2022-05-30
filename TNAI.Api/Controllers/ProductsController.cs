using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using TNAI.Api.Models.InputModels.Products;
using TNAI.Api.Models.OutputModels.Products;
using TNAI.Model.Entities;
using TNAI.Respository.Abstract;

namespace TNAI.Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            return Ok(Map(product));
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var products = await _productRepository.GetAllProductsAsync();

            if (!products.Any())
                return NotFound();

            return Ok(Map(products));
        }

        public async Task<IHttpActionResult> Post(ProductInputModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            var result = await _productRepository.SaveProductAsync(product);
            if (!result)
            {
                return InternalServerError();
            }

            var productOutput = await _productRepository.GetProductAsync(product.Id);

            return Ok(Map(productOutput));
        }

        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] ProductInputModel model)
        {
            if (id <= 0)
                return BadRequest();

            if (model == null)
                return BadRequest("Model is null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
                return NotFound();

            product.Name = model.Name;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            var result = await _productRepository.SaveProductAsync(product);
            if (!result)
            {
                return InternalServerError();
            }

            return Ok(Map(product));
        }

        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (id <= 0)
                return BadRequest();

            //var product = await _productRepository.GetProductAsync(id);

            //if (product == null)
            //    return NotFound();

            var result = await _productRepository.DeleteProductAsync(id);
            if (!result)
            {
                return InternalServerError();
            }

            return Ok();
        }

        private ProductOutputModel Map(Product product)
        {
            return new ProductOutputModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };
        }

        private List<ProductOutputModel> Map(List<Product> products)
        {
            var result = new List<ProductOutputModel>();
            foreach (var product in products)
            {
                result.Add(new ProductOutputModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name
                });
            }

            return result;
        }
    }
}
