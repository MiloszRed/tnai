using AutoMapper;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TNAI.API.Models.InputModels.Category;
using TNAI.API.Models.OutputModels.Category;
using TNAI.Model.Entities;
using TNAI.Repository.Abstract;

namespace TNAI.API.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get selected category.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <returns>Details</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Category details", typeof(CategoryOutputModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empyt object")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var product = await _categoryRepository.GetCategoryAsync(id);

            if (product == null)
                return NotFound();

            var outputProduct = _mapper.Map<CategoryOutputModel>(product);

            return Ok(outputProduct);
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            if (categories == null || !categories.Any())
                return NotFound();

            var outputCategories = new List<CategoryOutputModel>();
            foreach (var product in categories)
            {
                //outputProducts.Add(new CategoryOutputModel()
                //{
                //    Id = product.Id,
                //    Name = product.Name
                //});

                outputCategories.Add(_mapper.Map<CategoryOutputModel>(product));
            }

            return Ok(outputCategories);
        }


        public async Task<IHttpActionResult> Post(CategoryInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be a null.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = _mapper.Map<Category>(model);

            var result = await _categoryRepository.SaveCategoryAsync(newCategory);

            if (!result)
                return InternalServerError();

            return Ok();

        }

        /// <param name="id">Id kategorii, którą chcemy zmienić.</param>
        /// <param name="model">Nowe dane kategorii.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(int id, CategoryInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be a null.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _categoryRepository.GetCategoryAsync(id);

            if (product == null)
                return NotFound();

            product.Name = model.Name;

            var result = await _categoryRepository.SaveCategoryAsync(product);

            if (!result)
                return InternalServerError();

            return Ok();
        }


        public async Task<IHttpActionResult> Delete(int id)
        {
            var product = await _categoryRepository.GetCategoryAsync(id);
            if (product == null)
                return NotFound();

            var result = await _categoryRepository.DeleteCategoryAsync(product);
            if (!result)
                return InternalServerError();

            return Ok();

        }

    }

}
