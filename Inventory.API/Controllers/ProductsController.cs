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
    public class ProductsController : Controller
    {
        private readonly DBContext _context;
        private IProductRepository _productRespository;


        public ProductsController()
        {
            this._productRespository = new ProductRepository(_context);
        }

        public ProductsController(IProductRepository productRepository)
        {
            this._productRespository = productRepository;
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(_productRespository.GetProducts());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = _productRespository.GetProductById((int)id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ProductID,Name,Description,Cost,Price,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productRespository.InsertProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = _productRespository.GetProductById((int)id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ProductID,Name,Description,Cost,Price,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productRespository.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _productRespository.GetProductById((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productRespository.DeleteProduct((int)id);
            return RedirectToAction(nameof(Index));
        }

    }
}
