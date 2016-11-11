using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystem
{
    public class Master_Schedule
    {
        public int Schedule_ID { get; set; }
        public decimal Shift_Length_Minutes { get; set; }
        public TimeSpan Shift_Start { get; set; }
        public TimeSpan End_Shift { get; set; }
        public int Lunch { get; set; }
        public DateTime Timestamp { get; set; }
    }
}