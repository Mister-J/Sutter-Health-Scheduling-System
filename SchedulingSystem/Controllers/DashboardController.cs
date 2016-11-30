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
        // GET: Dashboard
        //Index is the main dashboard page where you can see today's schedule and future schedules. 
        public ActionResult Index()
        {
            var connectionInfo = new ConnectToLdap();
            connectionInfo.clientConnection("administrator", "sutterlogin.webhop.net", "theverge55\\", "sutterlogin.webhop.net:2037");
            bool test = connectionInfo.validateUserByBind("administrator", "theverge55\\");

      
            var dashboardData = new DashboardViewModel();
            string[] employeeTest = dashboardData.listofEmployees();
            
            return View(dashboardData);
        }

        [HttpPost]
        public ActionResult ChangeSchedule(string datetimepicker1)
        {
            var dashboardData = new DashboardViewModel();
            string test = datetimepicker1;
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
            return View(dashboardData);
        }

        public ActionResult ChangeSchedule()
        {
            return View();
        }

        //this method creates an ldap connection to our active directory
        //still in development. 
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