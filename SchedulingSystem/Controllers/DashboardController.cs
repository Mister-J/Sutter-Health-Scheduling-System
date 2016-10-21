using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchedulingSystem.Models;
using System.Data.SqlClient;

namespace SchedulingSystem.Controllers
{
    public class DashboardController : Controller
    {

        // GET: Dashboard
        public ActionResult Index()
        {
            //SqlConnection conn = new SqlConnection("Server=WIN-GU157GQNLCR;Database=Sutter_Schedule;uid=Administrator;password=4aBVh5iDm");
            //conn.Open();
            // if (conn.Open == true)
            //{

            // }
            DashboardViewModel dashboardData = new DashboardViewModel();

            return View(dashboardData);
        }
    }
}