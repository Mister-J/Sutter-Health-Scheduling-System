using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SchedulingSystem.Models;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;

namespace SchedulingSystem
{

    public static class SqlStatements
    {
        private static Schedule_Lines[] schedule;
        public static string connectionStatus;
        private static SqlConnection conn;
        private static Employee[] employee;
        private static List<EmployeeJson> employeeList;
        private static List<ScheduleJSon> scheduleList;
        private static string[] listOfEmployees;

        public static SqlConnection ConnectToSql()
        {
            conn = new SqlConnection("server=sutterdb.cdnagtbeyki3.us-west-2.rds.amazonaws.com,1433; database=SutterDB;user id=sutterdbadmin;password=M6)wo697s*W");
            conn.Open();
            connectionStatus = "Connection OK";
            return conn;
        }

        public static string ConnectionStatus()
        {
            return connectionStatus;
        }

        public static string[] listofEmps(SqlCommand employeeListCommand, SqlCommand countEmp)
        {
            if (conn.State == ConnectionState.Open)
            {
                int empCount = (int)countEmp.ExecuteScalar();
                listOfEmployees = new string[empCount];
                using (SqlDataReader employeeReader = employeeListCommand.ExecuteReader())
                {
                    int i = 0;
                    while (employeeReader.HasRows)
                    {
                        while (employeeReader.Read())
                        {
                            string s = employeeReader.GetInt32(0).ToString() + " - " + employeeReader.GetString(1) + " " + employeeReader.GetString(2);
                            listOfEmployees[i] = s;
                            i++;

                        }
                        employeeReader.NextResult();
                    }
                }
            }
            return listOfEmployees;
        }

        public static List<ScheduleJSon> retrieveSchedules(SqlCommand scheduleCommand, SqlCommand scheduleCountCommand)
        {
            if (conn.State == ConnectionState.Open)
            {
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
                ScheduleJSon[] theSchedule = new ScheduleJSon[scheduleCount];
                scheduleList = new List<ScheduleJSon>();
                for (int i = 0; i <= scheduleCount - 1; i++)
                {
                    theSchedule[i] = new ScheduleJSon();
                    theSchedule[i].id = i + 1;
                    theSchedule[i].resourceId = schedule[i].Schedule_ID;
                    theSchedule[i].start = String.Format("{0:s}", schedule[i].Shift_Start);
                    theSchedule[i].end = String.Format("{0:s}", schedule[i].End_Shift);
                    theSchedule[i].title = employee[i].Emp_First_Name;
                    scheduleList.Add(theSchedule[i]);
                }
                EmployeeJson[] theEmployees = new EmployeeJson[scheduleCount];
                employeeList = new List<EmployeeJson>();
                for (int i = 0; i <= scheduleCount - 1; i++)
                {
                    theEmployees[i] = new EmployeeJson();
                    theEmployees[i].id = schedule[i].Schedule_ID;
                    theEmployees[i].title = employee[i].Emp_First_Name;
                    employeeList.Add(theEmployees[i]);


                }
            }
            return scheduleList;
        }

        public static List<EmployeeJson> getEmployeeJson()
        {
            return employeeList;
        }
    }
}