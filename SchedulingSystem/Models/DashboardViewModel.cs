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

        public void UpdateSchedule(string theDate)
        {
            SqlConnection conn = SqlStatements.ConnectToSql();
            if (conn.State == ConnectionState.Open) {
                SqlCommand UpdateSchedule = new SqlCommand("UPDATE Master_Schedule SET Shift_Start = @ParamStartDate WHERE Schedule_ID = 888", conn);
                UpdateSchedule.Parameters.AddWithValue("@ParamStartDate", theDate);
                UpdateSchedule.ExecuteNonQuery();
            }
        }

        public void CreateSchedule(string startDate, string endDate)
        {
            SqlConnection conn = SqlStatements.ConnectToSql();
            if (conn.State == ConnectionState.Open)
            {
                SqlCommand CreateSchedule = new SqlCommand("INSERT INTO Master_Schedule (Schedule_ID, Shift_Length_Minutes, Shift_Start, End_Shift, Timestamp) VALUES(@ParamID, @ParamLengthMinutes, @ParamShiftStart, @ParamEndShift, @ParamTimestamp)", conn);
                CreateSchedule.Parameters.AddWithValue("@ParamShiftStart", startDate);
                CreateSchedule.Parameters.AddWithValue("@ParamEndShift", endDate);
                CreateSchedule.Parameters.AddWithValue("@ParamTimestamp", DateTime.Today);
                CreateSchedule.Parameters.AddWithValue("@ParamID", 1);
                CreateSchedule.Parameters.AddWithValue("@ParamLengthMinutes", 480.00);
                CreateSchedule.ExecuteNonQuery();
            }
        }

        public string listofEmployees()
        {
            string empNameString;
            SqlConnection conn = SqlStatements.ConnectToSql();
            SqlCommand listEmpCommand = new SqlCommand("SELECT Emp_First_Name from Employees", conn);
            SqlCommand countEmpCommand = new SqlCommand("SELECT count(*) FROM Employees", conn);
            string[] employees = SqlStatements.listofEmps(listEmpCommand, countEmpCommand);
            empNameString = JsonConvert.SerializeObject(employees, Formatting.Indented);
            
            return empNameString;
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