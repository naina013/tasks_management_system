using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T.Models;
using T.REPO;

namespace T.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase

    {
        private readonly TMSContext _context;
       

        public ValuesController(TMSContext context)
        {
            _context = context;
        }
        // GET: api/Tasks/?date=2020-07-10
        /// <summary>
        /// Retrieve the CSV of all tasks whose state is "inProgress" for some date.
        /// </summary>
        /// <param name="date">Enter the date for which list of tasks inProgress is required.</param>
        /// <returns>CSV File</returns>
        [HttpGet]
       
        public async Task<IActionResult> GetCSV(DateTime date)
        {
            StateCheck state = new StateCheck();
            List<Models.Task> inProgressData = await state.getCsvData(date, _context);



            Type itemType = typeof(Models.Task);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .OrderBy(p => p.Name);

            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, leaveOpen: true))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in inProgressData)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }

            stream.Position = 0;
            return File(stream, "application/octet-stream", "Reports.csv");


        }

    }
}
