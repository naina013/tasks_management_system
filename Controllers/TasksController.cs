using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T.Models;

namespace T.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TasksController : ControllerBase 
    {

        private readonly TMSContext _context;
        public TasksController(TMSContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        /// <summary>
        /// Retrieve the list of all tasks.
        /// </summary>
        /// <returns>List</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTask()
        {
            return await _context.Task.ToListAsync();
        }

        // GET: api/Tasks/5
        /// <summary>
        /// Retrieve the task by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired task.</param>
        /// <returns> Task</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {

            var task = await _context.Task.FindAsync(id);
            List<Subtask> subtasks = await (
                                            from st in _context.Subtask
                                            where id == st.TaskId
                                            select new Subtask
                                            {
                                                SubName = st.SubName,
                                                SubDesc = st.SubDesc,
                                                SubSdate = st.SubSdate,
                                                SubFdate = st.SubFdate,
                                                SubState = st.SubState
                                            }).ToListAsync();
            task.Subtask = subtasks;
            

            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the task by their ID.
        /// </summary>
        /// <param name="id">ID of the task to be updated.</param>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Models.Task task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Insert a new task.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            _context.Task.Add(task);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskExists(task.TaskId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTask", new { id = task.TaskId }, task);
        }

        // DELETE: api/Tasks/5
        /// <summary>
        /// Delete the task by their ID.
        /// </summary>
        /// <param name="id">Enter the ID of the task to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Task>> DeleteTask(int id)
        {
            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.TaskId == id);
        }
    }
}
