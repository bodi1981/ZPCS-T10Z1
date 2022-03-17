using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _unitOfWork.Category.GetCategories(userId);
        }

        public Category GetCategory(int categoryId, string userId)
        {
            return _unitOfWork.Category.GetCategory(categoryId, userId);
        }

        public void EditCategory(Category category)
        {
            _unitOfWork.Category.EditCategory(category);
        }

        public void AddCategory(Category category)
        {
            _unitOfWork.Category.AddCategory(category);
        }

        public void DeleteCategory(int categoryId, string userId)
        {
            _unitOfWork.Category.DeleteCategory(categoryId, userId);
        }

        public void Complete()
        {
            _unitOfWork.Complete();
        }
    }
}
