using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T.Models;
using T.REPO;

namespace T.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtasksController : ControllerBase
    {
        private readonly TMSContext _context;
        private StateCheck state = new StateCheck();

        public SubtasksController(TMSContext context)
        {
            _context = context;
        }

        // GET: api/Subtasks
        /// <summary>
        /// Retrieve the list of all subtasks.
        /// </summary>
        /// <returns>List</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subtask>>> GetSubtask()
        {
            return await _context.Subtask.ToListAsync();
        }

        // GET: api/Subtasks/5
        /// <summary>
        /// Retrieve the subtask by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired subtask.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Subtask>> GetSubtask(int id)
        {
            var subtask = await _context.Subtask.FindAsync(id);

            if (subtask == null)
            {
                return NotFound();
            }

            return subtask;
        }

        // PUT: api/Subtasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the subtask by their ID.
        /// </summary>
        /// <param name="id">ID of the subtask to be updated.</param>
        /// <param name="subtask"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubtask(int id, Subtask subtask)
        {
            if (id != subtask.SubId)
            {
                return BadRequest();
            }

            _context.Entry(subtask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                state.manageState(subtask.TaskId, _context);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubtaskExists(id))
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

        // POST: api/Subtasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Insert a new subtask.
        /// </summary>
        /// <param name="subtask"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Subtask>> PostSubtask(Subtask subtask)
        {
            _context.Subtask.Add(subtask);
            try
            {
                await _context.SaveChangesAsync();
                state.manageState(subtask.TaskId, _context);
            }
            catch (DbUpdateException)
            {
                if (SubtaskExists(subtask.SubId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubtask", new { id = subtask.SubId }, subtask);
        }

        // DELETE: api/Subtasks/5
        /// <summary>
        /// Delete the subtask by their ID.
        /// </summary>
        /// <param name="id">Enter the ID of the subtask to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subtask>> DeleteSubtask(int id)
        {
            var subtask = await _context.Subtask.FindAsync(id);
            if (subtask == null)
            {
                return NotFound();
            }

            _context.Subtask.Remove(subtask);
            await _context.SaveChangesAsync();

            return subtask;
        }

        private bool SubtaskExists(int id)
        {
            return _context.Subtask.Any(e => e.SubId == id);
        }
    }
}
