using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystem.Models
{
    public class Employee
    {
        public int Emp_ID { get; set; }
        public string Emp_First_Name { get; set; }
        public string Emp_Last_Name { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Phone_Number { get; set; }
        public int Departments_Dept_No { get; set; }
        public int Employee_Shift_Emp_ID { get; set; }
        public int Employee_Shift_Emp_Shift_ID { get; set; }




    }
}