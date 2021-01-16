using Category.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Category.API.Repository
{
    public interface ICategoryRepository
    {
        ICollection<CategoryModel> GetCategories();
        CategoryModel GetCategory(int CategoryID);
        bool ExistCategory(string name);
        bool ExistCategory(int CategoryID);
        bool CreateCategory(CategoryModel category);
        bool UpdateCategory(CategoryModel category);
        bool DeleteCategory(CategoryModel category);
        bool Save();
    }
}
