using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using MyTasks.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private IApplicationDbContext _dbContext;
        public TaskRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Task> GetTasks(string userId, string title = null, int categoryId = 0, bool isExecuted = false)
        {
            var tasks = _dbContext.Tasks
                    .Include(x => x.Category)
                    .Where(x => x.UserId == userId && x.IsExecuted == isExecuted).AsEnumerable();

            if (categoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                tasks = tasks.Where(x => x.Title.Contains(title));

            return tasks.OrderBy(x => x.Term).ToList();
        }

        public Task GetTask(int taskId, string userId)
        {
            return _dbContext.Tasks.Single(x => x.Id == taskId && x.UserId == userId);
        }

        public void EditTask(Task task)
        {
            var taskToEdit = _dbContext.Tasks.Single(x => x.Id == task.Id && x.UserId == task.UserId);

            taskToEdit.Title = task.Title;
            taskToEdit.CategoryId = task.CategoryId;
            taskToEdit.Description = task.Description;
            taskToEdit.Term = task.Term;
            taskToEdit.IsExecuted = task.IsExecuted;
        }

        public void AddTask(Task task)
        {
            _dbContext.Tasks.Add(task);
        }

        public void Delete(int taskId, string userId)
        {
            var taskToDelete = _dbContext.Tasks.Single(x => x.Id == taskId && x.UserId == userId);
            _dbContext.Tasks.Remove(taskToDelete);
        }

        public void Finish(int taskId, string userId)
        {
            var taskToFinish = _dbContext.Tasks.Single(x => x.Id == taskId && x.UserId == userId);
            taskToFinish.IsExecuted = true;
        }
    }
}
