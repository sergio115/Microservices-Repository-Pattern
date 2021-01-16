using Category.API.Models;
using Category.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Category.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var listCategories = _categoryRepository.GetCategories();
            return Ok(listCategories);
        }

        [HttpGet("{categoryID:int}", Name = "GetCategory")]
        public IActionResult GetCategory(int categoryID)
        {
            var itemCategory = _categoryRepository.GetCategory(categoryID);

            if (itemCategory == null)
            {
                return NotFound();
            }

            return Ok(itemCategory);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryModel category)
        {
            if (category == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.ExistCategory(category.Name))
            {
                ModelState.AddModelError("", "La categoria ya existe");
                return StatusCode(404, ModelState);
            }

            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryID = category.CategoryID }, category);
        }

        [HttpPatch("{categoryID:int}", Name = "UpdateCategory")]
        public IActionResult UpdateCategory(int categoryID, [FromBody] CategoryModel category)
        {
            if (category == null || categoryID != category.CategoryID)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryID:int}", Name = "DeleteCategory")]
        public IActionResult DeleteCategory(int categoryID)
        {
            if (!_categoryRepository.ExistCategory(categoryID))
            {
                return NotFound();
            }

            var category = _categoryRepository.GetCategory(categoryID);

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", $"Algo salio mal al intentar borrar el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
