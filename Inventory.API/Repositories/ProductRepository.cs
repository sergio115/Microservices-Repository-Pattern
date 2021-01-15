using Inventory.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.API.Repositories
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly DBContext _dBContext;
        private bool disposed = false;

        public ProductRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dBContext.Products.ToList();
        }

        public Product GetProductById(int ProductID)
        {
            return _dBContext.Products.Find(ProductID);
        }

        public void InsertProduct(Product product)
        {
            _dBContext.Products.Add(product);
            Save();
        }

        public void DeleteProduct(int ProductID)
        {
            Product product = _dBContext.Products.Find(ProductID);
            _dBContext.Products.Remove(product);
            Save();
        }

        public void UpdateProduct(Product product)
        {
            _dBContext.Entry(product).State = EntityState.Modified;
            Save();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dBContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _dBContext.SaveChanges();
        }
    }
}
