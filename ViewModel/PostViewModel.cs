using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.ViewModel
{
    public class PostViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }

        public DateTime sdate { get; set; }
        public DateTime fdate { get; set; }
        public string state { get; set; }

        List<Subtask> s = new List<Subtask>();
            
    }
}
