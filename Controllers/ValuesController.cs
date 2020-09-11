using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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



            //var stream = new MemoryStream();
            //using (var writeFile = new StreamWriter(stream, leaveOpen: true))
            //{
            //  var csv = new CsvWriter(writeFile, true);
            //csv.Configuration.RegisterClassMap<GroupReportCSVMap>();
            //csv.WriteRecords(reportCSVModels);
            //}
            //stream.Position = 0; //reset stream



            //return inProgressData;
        }

    }
}
