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
           // Response.Write(test);
            
            return View(dashboardData);
        }

        public ActionResult Login()
        {
            

            return View();
        }

        public ActionResult CreateSchedule()
        {
            return View();
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