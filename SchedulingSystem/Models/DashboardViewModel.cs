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
        public Schedule_Lines[] schedule;
        public Employee testEmployee = new Employee();
        public DateTime todayDate = DateTime.Today;
        public string connectionStatus;
        public string testVariables;
        private SqlConnection conn;
        public Employee[] employee;
        public string jsonString;
        public string resourceString = "[{ id: 1, title: abc }]";
        public List<EmployeeJson> employeeList;
        public List<ScheduleJSon> scheduleList;
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
                //SqlCommand selectCommand = new SqlCommand("SELECT Emp_First_Name FROM Employees", conn);
               // SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) From Employees", conn);
                SqlCommand scheduleCommand = new SqlCommand("SELECT Schedule_Line_ID, Master_Schedule_Schedule_ID, Shift_Start, End_Shift, Emp_First_Name FROM Schedule_Lines, Master_Schedule, Employees where Schedule_ID = Master_Schedule_Schedule_ID and Emp_ID=Employees_Emp_ID", conn);
                SqlCommand scheduleCountCommand = new SqlCommand("SELECT COUNT(*) FROM Schedule_Lines, Master_Schedule, Employees where Schedule_ID = Master_Schedule_Schedule_ID and Emp_ID=Employees_Emp_ID", conn);
                int scheduleCount = (int)scheduleCountCommand.ExecuteScalar();
                employee = new Employee[scheduleCount];
                schedule = new Schedule_Lines[scheduleCount];



                using (SqlDataReader scheduleReader = scheduleCommand.ExecuteReader())
                {
                    int i = 0;
                    while (scheduleReader.HasRows)
                    {


                        while (scheduleReader.Read())
                        {

                            schedule[i] = new Schedule_Lines();
                            employee[i] = new Employee();
                            schedule[i].Schedule_ID = scheduleReader.GetInt32(0);
                            schedule[i].Exception_ID = scheduleReader.GetInt32(1);
                            schedule[i].Shift_Start = scheduleReader.GetDateTime(2);
                            schedule[i].End_Shift = scheduleReader.GetDateTime(3);
                            employee[i].Emp_First_Name = scheduleReader.GetString(4);
                            i++;
                        }
                        scheduleReader.NextResult();
                    }




                }

                EmployeeJson[] testEmployee = new EmployeeJson[scheduleCount];
                employeeList = new List<EmployeeJson>();
                for (int i = 0; i <= scheduleCount - 1; i++)
                {
                    testEmployee[i] = new EmployeeJson();
                    testEmployee[i].id = schedule[i].Schedule_ID;
                    testEmployee[i].title = employee[i].Emp_First_Name;
                    employeeList.Add(testEmployee[i]);


                }

                ScheduleJSon[] theSchedule = new ScheduleJSon[scheduleCount];
                scheduleList = new List<ScheduleJSon>();
                for (int i = 0; i<=scheduleCount - 1; i++)
                {
                    theSchedule[i] = new ScheduleJSon();
                    theSchedule[i].id = i + 1;
                    theSchedule[i].resourceId = schedule[i].Schedule_ID;
                    theSchedule[i].start = String.Format("{0:s}", schedule[i].Shift_Start);
                    theSchedule[i].end = String.Format("{0:s}", schedule[i].End_Shift);
                    theSchedule[i].title = employee[i].Emp_First_Name;
                    scheduleList.Add(theSchedule[i]);
                }

                
                jsonString = JsonConvert.SerializeObject(employeeList, Formatting.Indented);
                scheduleJsonString = JsonConvert.SerializeObject(scheduleList, Formatting.Indented);
               
                
                
                

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
    public int resourceId { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string title { get; set; }

}