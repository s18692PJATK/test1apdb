using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test1apdb.Models;

namespace test1apdb.Services
{
     public interface ITaskService
    {
        public List<MyTask> GetTasks(int IdProject);
        public int AddTask(MyTask task);
        
    }
}
