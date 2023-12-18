using AutoMapper;
using Inventory_Manager.Helpers;
using Inventory_Manager.Models;
using InventoryDomain;
using InventoryDomain.DataTransferObjects;
using InverntoryData.Models;
using InverntoryData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory_Manager.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            return StatusCode(StatusCodes.Status200OK, new SuccessResult<IEnumerable<CategoryDTO>>() { success = true, result = mapper.Map<IEnumerable<CategoryDTO>>(categoryService.GetCategories()) });
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Category Id Must be provided." }, success = false });
            }
            var category = categoryService.GetCategoryById(id.Value);

            if (category == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new FailResult() { messages = new string[] { "Category not found." }, success = false });

            }
            return StatusCode(StatusCodes.Status200OK, new SuccessResult<CategoryDTO>() { success = true, result = mapper.Map<CategoryDTO>(category) });
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryModel model)
        {
            if (model == null) return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Category data is required." }, success = false });

            if (model.Name == null) return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Category Name is required." }, success = false });

            if (categoryService.CreateCategory(model))
            {
                return StatusCode(StatusCodes.Status200OK, new SuccessResult<IEnumerable<Product>>() { success = true, messages = new string[] { "Category Created." } });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new FailResult() { messages = new string[] { "An error occured while creating the category. please try again." }, success = false });
        }
    }
}
