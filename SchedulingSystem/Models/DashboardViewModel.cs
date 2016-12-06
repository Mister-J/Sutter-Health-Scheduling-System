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

//This is the main model for the dashboard view

namespace SchedulingSystem.Models
{
    public class DashboardViewModel
    {
        //GetJson is a method that returns a json string which is used for the calendar on the dashboard
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

        //EmployeeJson returns a json string of all the employees in the dashboard to be used with the javascript calendar.
        public string EmployeeJson()
        {
            string employeeJsonString;
            List<EmployeeJson> Employees = SqlStatements.getEmployeeJson();
            employeeJsonString = JsonConvert.SerializeObject(Employees, Formatting.Indented);
            return employeeJsonString;
        }

        //this method is used to change the master schedule. This isn't fully implemented yet.
        //still going through testing. 
        public void UpdateSchedule(string theDate)
        {
            SqlConnection conn = SqlStatements.ConnectToSql();
            if (conn.State == ConnectionState.Open) {
                SqlCommand UpdateSchedule = new SqlCommand("UPDATE Master_Schedule SET Shift_Start = @ParamStartDate WHERE Schedule_ID = 888", conn);
                UpdateSchedule.Parameters.AddWithValue("@ParamStartDate", theDate);
                UpdateSchedule.ExecuteNonQuery();
            }
        }

        //this method creates the master schedule as well as the schedule lines for that 
        //particular master schedule
        public void CreateSchedule(string startDate, string endDate, string empName)
        {
            int Emp_ID = 9999;
            int MasterID = 99999;
            SqlConnection conn = SqlStatements.ConnectToSql();
            DateTime todayDate = DateTime.Now;
            if (conn.State == ConnectionState.Open)
            {
                SqlCommand CreateSchedule = new SqlCommand("INSERT INTO Master_Schedule (Shift_Length_Minutes, Shift_Start, End_Shift, Timestamp) VALUES(@ParamLengthMinutes, @ParamShiftStart, @ParamEndShift, @ParamTimestamp)", conn);
                CreateSchedule.Parameters.AddWithValue("@ParamShiftStart", startDate);
                CreateSchedule.Parameters.AddWithValue("@ParamEndShift", endDate);
                CreateSchedule.Parameters.AddWithValue("@ParamTimestamp", todayDate);
                //CreateSchedule.Parameters.AddWithValue("@ParamID", 1);
                CreateSchedule.Parameters.AddWithValue("@ParamLengthMinutes", 480.00);
                CreateSchedule.ExecuteNonQuery();
                SqlCommand CreateScheduleLines = new SqlCommand("INSERT INTO Schedule_Lines (Shift_Minutes, Timestamp, Employees_Emp_ID, Master_Schedule_Schedule_ID) VALUES(@ParamMinutes, @ParamTimeStamp, @ParamEmpID, @ParamMasterID)", conn);
                SqlCommand getEmployeeName = new SqlCommand("Select Emp_ID, Schedule_ID FROM Employees, Master_Schedule where Emp_First_Name = @ParamEmpName and Shift_Start = @ParamStart and End_Shift = @ParamEnd and Timestamp = @ParamNow", conn);
                getEmployeeName.Parameters.AddWithValue("@ParamEmpName", empName);
                getEmployeeName.Parameters.AddWithValue("@ParamStart", startDate);
                getEmployeeName.Parameters.AddWithValue("@ParamEnd", endDate);
                getEmployeeName.Parameters.AddWithValue("@ParamNow", todayDate);
                using (SqlDataReader employeeNameReader = getEmployeeName.ExecuteReader())
                {
                    while (employeeNameReader.Read())
                    {
                        Emp_ID = employeeNameReader.GetInt32(0);
                        MasterID = employeeNameReader.GetInt32(1);
                    }
                    
                }
                //CreateScheduleLines.Parameters.AddWithValue("@ParamSchedID", MasterID);
                CreateScheduleLines.Parameters.AddWithValue("@ParamMinutes", 120.00);
                CreateScheduleLines.Parameters.AddWithValue("@ParamTimeStamp", todayDate);
                CreateScheduleLines.Parameters.AddWithValue("@ParamEmpID", Emp_ID);
                CreateScheduleLines.Parameters.AddWithValue("@ParamMasterID", MasterID);
                CreateScheduleLines.ExecuteNonQuery();
            }
        }

        //this method returns an array of all the employees
        public string[] listofEmployees()
        {
            SqlConnection conn = SqlStatements.ConnectToSql();
            SqlCommand listEmpCommand = new SqlCommand("SELECT Emp_First_Name FROM Employees", conn);
            SqlCommand countEmpCommand = new SqlCommand("SELECT count(*) FROM Employees", conn);
            string[] employees = SqlStatements.listofEmps(listEmpCommand, countEmpCommand);
            
            return employees;
        }
    }
}
        

//the EmployeeJson class is a json class to put our employee schedule data into two properties so the calendar can display the employees' shifts.
//the calendar uses two properties which are "id" and "title." 
//For id, we used the schedule id in the database.
//For title, we used the employees first name. 
public class EmployeeJson
{
    public int id { get; set; }
    public string title { get; set; }
}

//the ScheduleJson class is a json class in which we instantiate for the schedules in the database.
//This class is used so the calendar can render the data.
//id can be any number
//resourceID is the schedule id.
//start is the start time
//end is the end time
//title is the employees' first name that is working that shift. 

public class ScheduleJSon
{
    public int id { get; set; }
    public int resourceId { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string title { get; set; }

}