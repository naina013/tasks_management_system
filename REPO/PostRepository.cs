using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T.Models;
using T.REPO;
using Task = T.Models.Task;
using System.Text.Json.Serialization;

namespace T.REPO
{
    public class PostRepository : IPostRepository
    {
        TMSContext db;
        public PostRepository(TMSContext _db)
        {
            db = _db;
        }
        public bool Createsubtask(Subtask st)
        {
            if (db != null)
            {
                db.Subtask.Add(st);
                db.SaveChanges();
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        public bool Createtask(Task t)
        {
            if(db != null)
            {
                db.Task.Add(t);
                db.SaveChanges();
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        

        public bool Deletesubtask(Subtask st)
        {
            if (db != null)
            {
                db.Subtask.Remove(st);
                db.SaveChanges();
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        public bool Deletetask(Task t)
        {
            if (db != null)
            {
                db.Task.Remove(t);
                db.SaveChanges();
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        public ICollection<Subtask> ReadSubtask(int task_id)
        {
            if(db != null)
            {
                ICollection<Models.Subtask> subTaskList = new List<Models.Subtask>();
                subTaskList.Add(db.Subtask.Where(e => e.TaskId == task_id).FirstOrDefault());
                return subTaskList;

            }
            throw new NotImplementedException();
        }

        public Task Readtask(int task_id)
        {
            if(db != null)
            {
                return db.Task.FirstOrDefault(e => e.TaskId == task_id);
                
            }
            throw new NotImplementedException();
        }

        public bool Updatesubtask(Subtask st)
        {
            if (db != null)
            {
                db.Subtask.Update(st);
                db.SaveChanges();
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        public bool Updatetask(Task t)
        {
            if (db != null)
            {
                db.Task.Update(t);
                db.SaveChanges();
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        
    }
}
