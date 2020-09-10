using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
using Task = TMS.Models.Task;

namespace TMS.Repository
{
    public interface IPostRepository
    {
        public Boolean Createtask(Task t);
        public Task Readtask(int task_id);
        public Boolean Updatetask(Task t);
        public Boolean Deletetask(Task t);
        public Boolean Createsubtask(Subtask st);
        public Subtask ReadSubtask(int task_id);

        public Boolean Updatesubtask(Subtask st);
        public Boolean Deletesubtask(Subtask st);


    }
}
