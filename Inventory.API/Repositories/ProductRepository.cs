using Inventory.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _dBContext;

        public ProductRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public bool CreateProduct(Product product)
        {
            _dBContext.Products.Add(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            _dBContext.Products.Remove(product);
            return Save();
        }

        public bool ExistProduct(string name)
        {
            bool value = _dBContext.Products.Any(product => product.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistProduct(int ProductID)
        {
            return _dBContext.Products.Any(product => product.ProductID == ProductID);
        }

        public Product GetProduct(int ProductID)
        {
            return _dBContext.Products.FirstOrDefault(product => product.ProductID == ProductID);
        }

        public ICollection<Product> GetProducts()
        {
            return _dBContext.Products.OrderBy(product => product.Name).ToList();
        }

        public bool Save()
        {
            return _dBContext.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _dBContext.Products.Update(product);
            return Save();
        }
    }
}
