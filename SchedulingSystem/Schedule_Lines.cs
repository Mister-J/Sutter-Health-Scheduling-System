using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystem
{
    public class Schedule_Lines : Master_Schedule
    {
        public int Emp_ID { get; set; }
        public int Emp_Shift_ID { get; set; }
        public int Supervisor_ID { get; set; }
        public int Exception_ID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Supervisors_Supervisor_ID { get; set; }
        public int Master_Schedule_Schedule_ID { get; set; }
    }
}