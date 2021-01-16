using Inventory.API.Models;
using System;
using System.Collections.Generic;

namespace Inventory.API.Repositories
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int ProductID);
        bool ExistProduct(string name);
        bool ExistProduct(int ProductID);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
