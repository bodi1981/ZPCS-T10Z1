using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Persistence.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Task> GetTasks(string userId, string title = null, int categoryId = 0, bool isExecuted = false)
        {
            return _unitOfWork.Task.GetTasks(userId, title, categoryId, isExecuted);
        }

        public Task GetTask(int taskId, string userId)
        {
            return _unitOfWork.Task.GetTask(taskId, userId);
        }

        public void EditTask(Task task)
        {
            _unitOfWork.Task.EditTask(task);
        }

        public void AddTask(Task task)
        {
            _unitOfWork.Task.AddTask(task);
        }

        public void Delete(int taskId, string userId)
        {
            _unitOfWork.Task.Delete(taskId, userId);
        }
          
        public void Finish(int taskId, string userId)
        {
            _unitOfWork.Task.Finish(taskId, userId);
        }

        public void Complete()
        {
            _unitOfWork.Complete();
        }
    }
}
