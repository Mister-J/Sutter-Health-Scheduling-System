using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using SchedulingSystem.Models;
using System.Data;
using System.Configuration;



namespace SchedulingSystem.Models
{
    public class DashboardViewModel
    {
        public Master_Schedule testDashBoard = new Master_Schedule();
        public DateTime todayDate = DateTime.Today;
        public string connectionStatus;
        public string testVariables;
        private SqlConnection conn;
        public int x;
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
                    SqlCommand selectCommand = new SqlCommand("SELECT Schedule_ID FROM Master_Schedule WHERE Schedule_ID = 888", conn);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            
                            testDashBoard.Schedule_ID = (int)reader["Schedule_ID"];
                        
                        } else
                        {
                            
                        }
                        
                            
                            
                        
                    }
                    conn.Close();



                } else
            {
                connectionStatus = "Connection not ok";
            }

            }
           

        }


    }