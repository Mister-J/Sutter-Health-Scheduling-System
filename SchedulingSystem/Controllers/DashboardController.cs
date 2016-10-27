using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchedulingSystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace SchedulingSystem.Controllers
{
    public class DashboardController : Controller
    {
        public bool isLoggedIn = false;
        // GET: Dashboard
        public ActionResult Index()
        {
            try
            {
                SqlConnection conn = new SqlConnection("server=sutterdb.cdnagtbeyki3.us-west-2.rds.amazonaws.com,1433; database=SutterDB;user id=sutterdbadmin;password=M6)wo697s*W");
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Response.Write("Connection OK!");
                }
                
            } catch
            {
                Response.Write("No Connection!");
            }

            var connectionInfo = new ConnectToLdap();
            connectionInfo.clientConnection("a", "a", "a", "a");
            bool test = connectionInfo.validateUserByBind("test", "test");
            Response.Write(test);

            var dashboardData = new DashboardViewModel();

            return View(dashboardData);
        }

        public ActionResult Login()
        {
            

            return View();
        }

    }
}