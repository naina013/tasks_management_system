using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T.Models;
using Task = T.Models.Task;
using System.Text.Json.Serialization;

namespace T.REPO
{
    public interface IPostRepository
    {
        public Boolean Createtask(Task t);
        public Task Readtask(int task_id);
        public Boolean Updatetask(Task t);
        public Boolean Deletetask(Task t);
        public Boolean Createsubtask(Subtask st);
        //[JsonIgnore]
        public ICollection<Subtask> ReadSubtask(int task_id);

        public Boolean Updatesubtask(Subtask st);
        public Boolean Deletesubtask(Subtask st);


    }
}
