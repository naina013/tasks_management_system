using Microsoft.AspNetCore.Mvc;
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
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public DateTime TaskSdate { get; set; }
        public DateTime TaskFdate { get; set; }
        public string TaskState { get; set; }

        public virtual List<Subtask> Subtask { get; set; }

        public static implicit operator Task(ActionResult<Task> v)
        {
            throw new NotImplementedException();
        }
    }
}
