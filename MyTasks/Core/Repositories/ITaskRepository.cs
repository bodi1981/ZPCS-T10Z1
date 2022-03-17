using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Core.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetTasks(string userId, string title = null, int categoryId = 0, bool isExecuted = false);
        Task GetTask(int taskId, string userId);
        void EditTask(Task task);
        void AddTask(Task task);
        void Delete(int taskId, string userId);
        void Finish(int taskId, string userId);
    }
}
