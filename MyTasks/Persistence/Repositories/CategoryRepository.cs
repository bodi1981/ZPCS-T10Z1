using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IApplicationDbContext _dbContext;
        public CategoryRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _dbContext.Categories.Where(x => x.UserId == userId || x.UserId == null).OrderBy(x => x.Name).ToList();
        }

        public Category GetCategory(int categoryId, string userId)
        {
            return _dbContext.Categories.Single(x => x.Id == categoryId && x.UserId == userId);
        }

        public void EditCategory(Category category)
        {
            var categoryToEdit = _dbContext.Categories.Single(x => x.Id == category.Id && x.UserId == category.UserId);

            categoryToEdit.Name = category.Name;
        }

        public void AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
        }

        public void DeleteCategory(int categoryId, string userId)
        {
            var categoryToDelete = _dbContext.Categories.Single(x => x.Id == categoryId && x.UserId == userId);
            _dbContext.Categories.Remove(categoryToDelete);
        }
    }
}
