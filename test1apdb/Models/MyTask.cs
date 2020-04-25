using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test1apdb.Models
{
    public class MyTask
    {
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public string ProjectName{ get; set; }
        public string TaskTypeName { get; set; }
        public string AssignedName{ get; set; }
        public string CreatorName{ get; set; }
        public int IdProject { get; set; }
        public int IdTaskType { get; set; }
        public int IdAssignedTo { get; set; }
        public int IdCreator { get; set; }


    }
}
