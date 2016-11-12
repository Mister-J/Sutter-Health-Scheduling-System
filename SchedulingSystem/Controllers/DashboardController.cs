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
            connectionInfo.clientConnection("test", "csusdc01", "P@ssw0rd!", "test@csus.com");
            bool test = connectionInfo.validateUserByBind("test", "P@ssw0rd!");

      
            var dashboardData = new DashboardViewModel();
            dashboardData.ConnectToSql();
            Response.Write(dashboardData.connectionStatus);

            Response.Write(test);
            Response.Write(dashboardData.jsonString);
            Response.Write(dashboardData.scheduleJsonString);
            
            return View(dashboardData);
        }

        public ActionResult Login()
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