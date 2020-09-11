using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T.Models;

namespace T.Models
{
    public class TaskData
    {
        public Task task { get; set; }
        public List<Subtask> subTasks { get; set; }

    }
}
