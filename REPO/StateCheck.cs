using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T.Models;

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
                if (s.SubState.ToString().Equals("inProgress"))
                {
                    task.TaskState = "inProgress";
                    context.Update(task);
                    context.SaveChanges();
                    return;
                }
            }

            foreach (Subtask s in subtasks)
            {
                if (s.SubState.ToString().Equals("Planned"))
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
    }
}
