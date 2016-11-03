using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystem
{
    public class Master_Schedule
    {
        public int Schedule_ID { get; set; }
        public int Shift_Length_Minutes { get; set; }
        public DateTime Shift_Start { get; set; }
        public DateTime End_Shift { get; set; }
        public int Lunch { get; set; }
        public DateTime Timestamp { get; set; }
    }
}