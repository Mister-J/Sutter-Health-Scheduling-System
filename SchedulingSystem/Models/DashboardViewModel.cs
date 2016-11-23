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

        public void CreateSchedule(string startDate, string endDate, string empName)
        {
            int Emp_ID = 9999;
            int MasterID = 99999;
            SqlConnection conn = SqlStatements.ConnectToSql();
            DateTime todayDate = DateTime.Now;
            if (conn.State == ConnectionState.Open)
            {
                SqlCommand CreateSchedule = new SqlCommand("INSERT INTO Master_Schedule (Schedule_ID, Shift_Length_Minutes, Shift_Start, End_Shift, Timestamp) VALUES(@ParamID, @ParamLengthMinutes, @ParamShiftStart, @ParamEndShift, @ParamTimestamp)", conn);
                //SqlCommand CreateScheduleLines = new SqlCommand("INSERT INTO Schedule_Lines (Schedule_Line_ID, Shift_Minutes, Timestamp")
                CreateSchedule.Parameters.AddWithValue("@ParamShiftStart", startDate);
                CreateSchedule.Parameters.AddWithValue("@ParamEndShift", endDate);
                CreateSchedule.Parameters.AddWithValue("@ParamTimestamp", todayDate);
                CreateSchedule.Parameters.AddWithValue("@ParamID", 1);
                CreateSchedule.Parameters.AddWithValue("@ParamLengthMinutes", 480.00);
                CreateSchedule.ExecuteNonQuery();
                SqlCommand CreateScheduleLines = new SqlCommand("INSERT INTO Schedule_Lines (Schedule_Line_ID, Shift_Minutes, Timestamp, Employees_Emp_ID, Master_Schedule_Schedule_ID) VALUES(@ParamSchedID, @ParamMinutes, @ParamTimeStamp, @ParamEmpID, @ParamMasterID)", conn);
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
                CreateScheduleLines.Parameters.AddWithValue("@ParamSchedID", 1);
                CreateScheduleLines.Parameters.AddWithValue("@ParamMinutes", 120.00);
                CreateScheduleLines.Parameters.AddWithValue("@ParamTimeStamp", todayDate);
                CreateScheduleLines.Parameters.AddWithValue("@ParamEmpID", Emp_ID);
                CreateScheduleLines.Parameters.AddWithValue("@ParamMasterID", MasterID);
                CreateScheduleLines.ExecuteNonQuery();
            }
        }

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