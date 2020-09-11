using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T.Models;
using Task = T.Models.Task;

namespace T.REPO
{
    public class StateCheck
    {
        public async void manageState(int task_id, TMSContext context)
        {
            var task = await context.Task.FindAsync(task_id);
            List<Subtask> subtasks = await(
                                            from st in context.Subtask
                                            where task_id == st.TaskId
                                            select new Subtask
                                            {
                                                SubName = st.SubName,
                                                SubDesc = st.SubDesc,
                                                SubSdate = st.SubSdate,
                                                SubFdate = st.SubFdate,
                                                SubState = st.SubState
                                            }).ToListAsync();

            foreach(Subtask s in subtasks)
            {
                if (s.SubState.ToString().Trim().Equals("inProgress"))
                {
                    task.TaskState = "inProgress";
                    context.Update(task);
                    context.SaveChanges();
                    return;
                }
            }

            foreach (Subtask s in subtasks)
            {
                if (s.SubState.ToString().Trim().Equals("Planned"))
                {
                    task.TaskState = "Planned";
                    context.Update(task);
                    context.SaveChanges();
                    return;
                }
            }

            task.TaskState = "Completed";
            context.Update(task);
            context.SaveChanges();


        }
        public async Task<List<Task>> getCsvData(DateTime date, TMSContext context)
        {
            List<Task> task = await(
                                            from t in context.Task
                                            select new Task
                                            {
                                                TaskName = t.TaskName,
                                                TaskDesc = t.TaskDesc,
                                                TaskSdate = t.TaskSdate,
                                                TaskFdate = t.TaskFdate,
                                                TaskState = t.TaskState
                                            }).ToListAsync();
            List<Task> result = new List<Task>();
            
            foreach (Task s in task)
            {
                if(s.TaskState.ToString().Trim().Equals("inProgress") && s.TaskSdate == date.Date)
                {
                    result.Add(s);
                }
            }
            return result;
        }

    }
}
