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
        public Master_Schedule testDashBoard = new Master_Schedule();
        public Schedule_Lines schedule;
        public Employee testEmployee = new Employee();
        public DateTime todayDate = DateTime.Today;
        public string connectionStatus;
        public string testVariables;
        private SqlConnection conn;
        public Employee[] employee;
        public string jsonString;
        public string resourceString = "[{ id: 1, title: abc }]";
        public List<EmployeeJson> employeeList;
        public string scheduleJsonString;
        public DashboardViewModel ()
        {
           

        }

        public void ConnectToSql()
        {

             conn = new SqlConnection("server=sutterdb.cdnagtbeyki3.us-west-2.rds.amazonaws.com,1433; database=SutterDB;user id=sutterdbadmin;password=M6)wo697s*W");
             conn.Open();
             if (conn.State == ConnectionState.Open)
             {
                connectionStatus = "Connection OK";
                SqlCommand selectCommand = new SqlCommand("SELECT Emp_First_Name FROM Employees", conn);
                SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) From Employees", conn);
                SqlCommand scheduleCommand = new SqlCommand("SELECT * FROM Schedule_Lines, Master_Schedule where Schedule_Line_ID = 10001 and Schedule_ID = 888", conn);
                int count = (int)countCommand.ExecuteScalar();
                employee = new Employee[count];
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                    int i = 0;
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            
                            employee[i] = new Employee();
                            employee[i].Emp_First_Name = reader.GetString(0);
                            i++;


                        }
                        reader.NextResult();
                    }
                    
                            
                            
                        
                    }



                using (SqlDataReader scheduleReader = scheduleCommand.ExecuteReader())
                {
                    while (scheduleReader.Read())
                    {
                        
                        schedule = new Schedule_Lines();
                        schedule.Schedule_ID = (int)scheduleReader["Schedule_Line_ID"];
                        schedule.Exception_ID = (int)scheduleReader["Exception_ID"];
                        schedule.Shift_Start = (TimeSpan)scheduleReader["Shift_Start"];
                        schedule.End_Shift = (TimeSpan)scheduleReader["End_Shift"];
                    }




                }

                ScheduleJSon theSchedule = new ScheduleJSon();
                theSchedule.id = schedule.Schedule_ID;
                theSchedule.resourceID = schedule.Exception_ID;
                theSchedule.start = schedule.Shift_Start.ToString();//String.Format("{0:s}", schedule.Shift_Start);
                theSchedule.end = schedule.End_Shift.ToString();//String.Format("{0:s}", schedule.End_Shift);
                theSchedule.title = "test";


                EmployeeJson[] testEmployee = new EmployeeJson[count];
                employeeList = new List<EmployeeJson>();
                for (int i = 0; i <= count - 1; i++)
                {
                    testEmployee[i] = new EmployeeJson();
                    testEmployee[i].id = i;
                    testEmployee[i].title = employee[i].Emp_First_Name;
                    employeeList.Add(testEmployee[i]);


                }
                
                jsonString = JsonConvert.SerializeObject(employeeList, Formatting.Indented);
                scheduleJsonString = JsonConvert.SerializeObject(theSchedule, Formatting.Indented);
               
                
                
                

                conn.Close();



                } else
            {
                connectionStatus = "Connection not ok";
            }

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
    public int resourceID { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string title { get; set; }

}