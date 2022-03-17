using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;
using MyTasks.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MyTasks.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ITaskService _taskService;
        private ICategoryService _categoryService;
        
        public TaskController(ITaskService taskService, ICategoryService categoryService)
        {
            //_taskService = new TaskService(new UnitOfWork(dbContext));
            _taskService = taskService;
            _categoryService = categoryService;
        }

        public IActionResult Tasks()
        {
            var userId = User.GetUserId();

            var tasks = _taskService.GetTasks(userId);

            var vm = new TasksViewModel
            {
                Tasks = _taskService.GetTasks(userId),
                Categories = _categoryService.GetCategories(userId),
                FilterTasks = new FilterTasks()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Tasks(TasksViewModel vm)
        {
            var userId = User.GetUserId();

            var tasks = _taskService.GetTasks(userId, vm.FilterTasks.Title, vm.FilterTasks.CategoryId, vm.FilterTasks.IsExecuted);

            return PartialView("_TasksTable", tasks);
        }

        public IActionResult Task(int taskId)
        {
            var userId = User.GetUserId();

            var task = (taskId == 0) ? new Task { Id = 0, UserId = userId, Term = DateTime.Today } : _taskService.GetTask(taskId, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Categories = _categoryService.GetCategories(userId),
                Header = (taskId == 0) ? "Dodawanie nowego zadania" : "Edycja zadania"
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Task(Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Categories = _categoryService.GetCategories(userId),
                    Header = (task.Id == 0) ? "Dodawanie nowego zadania" : "Edycja zadania"
                };

                return View("Task", vm);
            }

            if (task.Id == 0)
                _taskService.AddTask(task);
            else
                _taskService.EditTask(task);

            _taskService.Complete();

            return RedirectToAction("Tasks");
        }

        [HttpPost]
        public IActionResult Delete(int taskId)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Delete(taskId, userId);
                _taskService.Complete();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Finish(int taskId)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(taskId, userId);
                _taskService.Complete();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }
    }
}
