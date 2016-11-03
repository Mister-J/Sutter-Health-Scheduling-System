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
            var connectionInfo = new ConnectToLdap();
            connectionInfo.clientConnection("a", "a", "a", "a");
            bool test = connectionInfo.validateUserByBind("test", "test");
  

            var dashboardData = new DashboardViewModel();
            dashboardData.ConnectToSql();
            Response.Write(dashboardData.connectionStatus);
            Response.Write(dashboardData.testVariables);
            Response.Write(dashboardData.testDashBoard.Schedule_ID);
            return View(dashboardData);
        }

        public ActionResult Login()
        {
            

            return View();
        }

    }
}