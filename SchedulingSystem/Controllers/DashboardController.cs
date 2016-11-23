using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchedulingSystem.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.DirectoryServices;


namespace SchedulingSystem.Controllers
{
    public class DashboardController : Controller
    {
        public bool isLoggedIn = false;
        // GET: Dashboard
        public ActionResult Index()
        {
            var connectionInfo = new ConnectToLdap();
            connectionInfo.clientConnection("administrator", "sutterlogin.webhop.net", "theverge55\\", "sutterlogin.webhop.net:2037");
            bool test = connectionInfo.validateUserByBind("administrator", "theverge55\\");

      
            var dashboardData = new DashboardViewModel();
            Response.Write(SqlStatements.connectionStatus);
            Response.Write(test);
            string[] employeeTest = dashboardData.listofEmployees();
            Response.Write(employeeTest);
           // Response.Write(test);
            
            return View(dashboardData);
        }

        [HttpPost]
        public ActionResult ChangeSchedule(string datetimepicker1)
        {
            var dashboardData = new DashboardViewModel();
            string test = datetimepicker1;
            Response.Write(test);
            dashboardData.UpdateSchedule(test);
            return View();
        }

        public ActionResult Login()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult CreateSchedule(string datetimepicker6, string datetimepicker7, string dropDownEmpNames)
        {
            var dashboardData = new DashboardViewModel();
            string test = dropDownEmpNames;
            dashboardData.CreateSchedule(datetimepicker6, datetimepicker7, test);

            return View(dashboardData);
        }

        public ActionResult CreateSchedule()
        {
            var dashboardData = new DashboardViewModel();
            Response.Write(dashboardData.listofEmployees());
            return View(dashboardData);
        }

        public ActionResult ChangeSchedule()
        {
            return View();
        }

        static DirectoryEntry createDirectoryEntry()
        {
            // create and return new LDAP connection with desired settings  

            DirectoryEntry ldapConnection = new DirectoryEntry("csus.dc01.com");
            ldapConnection.Path = "LDAP://OU=user,DC=csus.dc01.com";
            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

            return ldapConnection;
        }

    }
}