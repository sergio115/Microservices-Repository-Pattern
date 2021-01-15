using Inventory.API.Models;
using System;
using System.Collections.Generic;

namespace Inventory.API.Repositories
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int ProductID);
        void InsertProduct(Product product);
        void DeleteProduct(int ProductID);
        void UpdateProduct(Product product);
        void Save();
    }
}
