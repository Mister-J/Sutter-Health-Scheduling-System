using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using SchedulingSystem.Models;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;



namespace SchedulingSystem.Models
{
    public class DashboardViewModel
    {
        public string GetJson()
        {
            string scheduleJsonString;

            SqlConnection conn = SqlStatements.ConnectToSql();
            SqlCommand scheduleCommand = new SqlCommand("SELECT Schedule_Line_ID, Master_Schedule_Schedule_ID, Shift_Start, End_Shift, Emp_First_Name FROM Schedule_Lines, Master_Schedule, Employees where Schedule_ID = Master_Schedule_Schedule_ID and Emp_ID=Employees_Emp_ID", conn);
            SqlCommand scheduleCountCommand = new SqlCommand("SELECT COUNT(*) FROM Schedule_Lines, Master_Schedule, Employees where Schedule_ID = Master_Schedule_Schedule_ID and Emp_ID=Employees_Emp_ID", conn);
            List<ScheduleJSon> EmployeeSchedule = SqlStatements.retrieveSchedules(scheduleCommand, scheduleCountCommand);
            scheduleJsonString = JsonConvert.SerializeObject(EmployeeSchedule, Formatting.Indented);
            return scheduleJsonString;

        }

        public string EmployeeJson()
        {
            string employeeJsonString;
            List<EmployeeJson> Employees = SqlStatements.getEmployeeJson();
            employeeJsonString = JsonConvert.SerializeObject(Employees, Formatting.Indented);
            return employeeJsonString;
        }
    }
}
        


public class EmployeeJson
{
    public int id { get; set; }
    public string title { get; set; }
}

public class ScheduleJSon
{
    public int id { get; set; }
    public int resourceId { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string title { get; set; }

}