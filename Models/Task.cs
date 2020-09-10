using System;
using System.Collections.Generic;

namespace TMS.Models
{
    public partial class Task
    {
        public Task()
        {
            Subtask = new HashSet<Subtask>();
        }

        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public DateTime TaskSdate { get; set; }
        public DateTime TaskFdate { get; set; }
        public string TaskState { get; set; }

        public virtual ICollection<Subtask> Subtask { get; set; }
    }
}
