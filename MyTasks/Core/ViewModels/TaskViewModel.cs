using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Core.ViewModels
{
    public class TaskViewModel
    {
        public Task Task { get; set; }
        public string Header { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
