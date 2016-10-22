using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace SchedulingSystem.Models
{
    public class DashboardViewModel
    {
        public Master_Schedule testDashBoard;   
           
        public DashboardViewModel ()
        {

            testDashBoard = new Master_Schedule();
            testDashBoard.Schedule_ID = 0;
            

        }


    }
}