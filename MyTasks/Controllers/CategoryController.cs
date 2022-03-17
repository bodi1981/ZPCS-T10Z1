using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Categories()
        {
            var userId = User.GetUserId();
            var vm = new CategoriesViewModel
            {
                Categories = _categoryService.GetCategories(userId)
            };

            return View(vm);
        }

        public IActionResult Category(int categoryId)
        {
            var userId = User.GetUserId();
            var vm = new CategoryViewModel
            {
                Header = (categoryId == 0) ? "Dodawanie nowej kategorii" : "Edycja kategorii",
                Category = (categoryId == 0) ? new Category { Id = 0, UserId = userId } : _categoryService.GetCategory(categoryId, userId)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(CategoryViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                var vm = new CategoryViewModel
                {
                    Header = (cvm.Category.Id == 0) ? "Dodawanie nowej kategorii" : "Edycja kategorii",
                    Category = (cvm.Category.Id == 0) ? new Category { Id = 0, UserId = cvm.Category.UserId } : _categoryService.GetCategory(cvm.Category.Id, cvm.Category.UserId)
                };

                return View("Category", vm);
            }

            if (cvm.Category.Id == 0)
                _categoryService.AddCategory(cvm.Category);
            else
                _categoryService.EditCategory(cvm.Category);

            _categoryService.Complete();

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public IActionResult Delete(int categoryId)
        {
            try
            {
                var userId = User.GetUserId();
                _categoryService.DeleteCategory(categoryId, userId);
                _categoryService.Complete();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }
    }
}
