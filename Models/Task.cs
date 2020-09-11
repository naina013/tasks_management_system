using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T.Models
{
    public partial class Task
    {
        public Task()
        {
            Subtask = new List<Subtask>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public DateTime TaskSdate { get; set; }
        public DateTime TaskFdate { get; set; }
        public string TaskState { get; set; }

        public virtual List<Subtask> Subtask { get; set; }
    }
}
