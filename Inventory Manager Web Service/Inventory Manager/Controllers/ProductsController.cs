using AutoMapper;
using Inventory_Manager.Helpers;
using Inventory_Manager.Models;
using InventoryDomain;
using InventoryDomain.DataTransferObjects;
using InverntoryData;
using InverntoryData.Models;
using InverntoryData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory_Manager.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;


        public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Product>> GetAllProducts()
        //{
        //    return Ok(productService.GetAllProducts());
        //}

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = productService.GetProduct(id);

            if (product == null)
            {
                //return NotFound();
                return StatusCode(StatusCodes.Status404NotFound, new FailResult() { messages = new string[] { "Product was not found" }, success = false });
            }

            return StatusCode(StatusCodes.Status200OK, new SuccessResult<ProductDTO>() { success = true, result = mapper.Map<ProductDTO>(product) });
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(int? CategoryID)
        {
            if (CategoryID != null)
            {
                var category = categoryService.GetCategoryWithRelatedEntities(CategoryID.Value);

                if (category == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new FailResult() { messages = new string[] { "Category not found" }, success = false });
                }

                if (category.Products == null || category.Products.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new FailResult() { messages = new string[] { "No products found" }, success = false });
                }
                return StatusCode(StatusCodes.Status200OK, new SuccessResult<IEnumerable<ProductDTO>>() { success = true, result = mapper.Map<IEnumerable<ProductDTO>>(category.Products)  });
            } else
            {
                return StatusCode(StatusCodes.Status200OK, new SuccessResult<IEnumerable<ProductDTO>>() { success = true, result = mapper.Map<IEnumerable<ProductDTO>>(productService.GetAllProducts()) });

            }
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            if (model == null) return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Product data is required." }, success = false });

            if (model.CategoryId == null) return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Product category id is required." }, success = false }); 

            // var category = categoryRepository.Find(category => category.CategoryId == model.CategoryId).FirstOrDefault();

            var category = categoryService.GetCategoryById(model.CategoryId);

            if (category == null) StatusCode(StatusCodes.Status404NotFound, new FailResult() { messages = new string[] { $"cagegory with category id {model.CategoryId} was not found." }, success = false });

            if (productService.CreateProduct(model, category))
            {
                return StatusCode(StatusCodes.Status200OK, new SuccessResult<IEnumerable<Product>>() { success = true, messages = new string[] { "Product Created." } });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new FailResult() { messages = new string[] { "An error occured while creating the product. please try again." }, success = false });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int? id, ProductModel model)
        {
            if (model == null) return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Product data is required." }, success = false });

            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Product Id Must be provided." }, success = false });
            }

            if (model.CategoryId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new FailResult() { messages = new string[] { "Category Id Must be provided." }, success = false });
            }

            var product = productService.GetProduct(id.Value);

            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new FailResult() { messages = new string[] { "Product not found." }, success = false });
            }

            var category = categoryService.GetCategoryById(model.CategoryId);

            if (category == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new FailResult() { messages = new string[] { "Category not found." }, success = false });
            }


            if (productService.UpdateProduct(product, model, category))
            {
                return StatusCode(StatusCodes.Status200OK, new SuccessResult<IEnumerable<Product>>() { success = true, messages = new string[] { "Product Updated Successfully." } });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new FailResult() { messages = new string[] { "An error occured while updating the product. please try again." }, success = false });
        }

    }
}
