using System;
using System.Collections.Generic;

namespace TMS.Models
{
    public partial class Subtask
    {
        public int SubId { get; set; }
        public string SubName { get; set; }
        public string SubDesc { get; set; }
        public DateTime SubSdate { get; set; }
        public DateTime SubFdate { get; set; }
        public string SubState { get; set; }
        public int TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}
