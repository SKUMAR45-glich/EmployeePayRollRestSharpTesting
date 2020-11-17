using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpTesting
{
    //Class for Employees 
    public class Employee
    {
        public int id { get; set; }                                   //Auto GET-SET Properties 
        public string name { get; set; }
        public int salary { get; set; }
    }
}