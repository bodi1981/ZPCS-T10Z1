using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(string userId);
        Category GetCategory(int categoryId, string userId);
        void EditCategory(Category category);
        void AddCategory(Category category);
        void DeleteCategory(int categoryId, string userId);
        void Complete();
    }
}
