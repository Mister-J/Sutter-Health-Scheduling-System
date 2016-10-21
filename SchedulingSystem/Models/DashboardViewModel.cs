using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace SchedulingSystem.Models
{
    public class DashboardViewModel
    {
        public Master_Schedule testSchedule { get; set; }
        
        public DashboardViewModel ()
        {
            
            testSchedule.Schedule_ID = 0;
            testSchedule.Lunch = 1;

        }


    }
}