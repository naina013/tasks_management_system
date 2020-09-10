using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
using Task = TMS.Models.Task;

namespace TMS.Repository
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

        public Subtask ReadSubtask(int task_id)
        {
            if(db != null)
            {
                return db.Subtask.FirstOrDefault(e => e.TaskId == task_id);
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
