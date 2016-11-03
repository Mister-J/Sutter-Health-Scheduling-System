using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystem
{
    public class Employee
    {
        public int Emp_ID { get; set; }
        public int Super_ID { get; set; }
        public string Emp_First_Name { get; set; }
        public string Emp_Last_Name { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Phone_Number { get; set; }
        public int Departments_Dept_NO { get; set; }
    }
}