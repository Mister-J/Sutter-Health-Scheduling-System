using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystem
{
    public class Schedule_Lines : Master_Schedule
    {
        public int Schedule_Line_ID { get; set; }
        public int Exception_ID { get; set; }
        public int Employees_Emp_ID { get; set; }
        public string Shift_year { get; set; }
        public string Shfit_Month { get; set; }
        public string Shift_Day { get; set; }
  
    }
}