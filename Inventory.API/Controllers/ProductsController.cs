using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.API.Models;
using Inventory.API.Repositories;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private IProductRepository _productRespository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRespository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var listProducts = _productRespository.GetProducts();
            return Ok(listProducts);
        }

        [HttpGet("{productID:int}", Name = "GetProduct")]
        public IActionResult GetProduct(int productID)
        {

            var itemCategory = _productRespository.GetProduct(productID);

            if (itemCategory == null)
            {
                return NotFound();
            }

            return Ok(itemCategory);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(ModelState);
            }

            if (_productRespository.ExistProduct(product.Name))
            {
                ModelState.AddModelError("", "El producto ya existe");
                return StatusCode(404, ModelState);
            }

            if (!_productRespository.CreateProduct(product))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro {product.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProduct", new { productID = product.ProductID }, product);
        }

        [HttpPatch("{productID:int}", Name = "UpdateProduct")]
        public IActionResult UpdateProduct(int productID, [FromBody] Product product)
        {
            if (product == null || productID != product.ProductID)
            {
                return BadRequest(ModelState);
            }

            if (!_productRespository.UpdateProduct(product))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro {product.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productID:int}", Name = "DeleteProduct")]
        public IActionResult DeleteProduct(int productID)
        {
            if (!_productRespository.ExistProduct(productID))
            {
                return NotFound();
            }

            var product = _productRespository.GetProduct(productID);

            if (!_productRespository.DeleteProduct(product))
            {
                ModelState.AddModelError("", $"Algo salio mal al intentar borrar el registro {product.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
