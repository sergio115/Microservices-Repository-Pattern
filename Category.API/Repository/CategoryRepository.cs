using Category.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Category.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DBContext _dBContext;

        public CategoryRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public bool CreateCategory(CategoryModel category)
        {
            _dBContext.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(CategoryModel category)
        {
            _dBContext.Categories.Remove(category);
            return Save();
        }

        public bool ExistCategory(string name)
        {
            bool value = _dBContext.Categories.Any(category => category.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistCategory(int CategoryID)
        {
            return _dBContext.Categories.Any(category => category.CategoryID == CategoryID);
        }

        public ICollection<CategoryModel> GetCategories()
        {
            return _dBContext.Categories.OrderBy(category => category.Name).ToList();
        }

        public CategoryModel GetCategory(int CategoryID)
        {
            return _dBContext.Categories.FirstOrDefault(category => category.CategoryID == CategoryID);
        }

        public bool Save()
        {
            return _dBContext.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCategory(CategoryModel category)
        {
            _dBContext.Categories.Update(category);
            return Save();
        }
    }
}
