using MyTasks.Core;
using MyTasks.Core.Repositories;
using MyTasks.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _dbContext;
        public UnitOfWork(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Task = new TaskRepository(dbContext);
            Category = new CategoryRepository(dbContext);
        }

        public ITaskRepository Task { get; set; }
        public ICategoryRepository Category { get; set; }
        public void Complete()
        {
            _dbContext.SaveChanges();
        }
    }
}
