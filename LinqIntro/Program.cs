using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace LinqIntro
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            QueryEmployees();
            QuerySvcProcess();
            QueryTypes();
        }

        private static void QueryTypes()
        {
            IEnumerable<string> publicType =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass
                select t.FullName;
            Console.WriteLine("Printing all Class Names from this assembly");
            foreach (var p in publicType)
            {
                Console.WriteLine($"{p}");
            }
        }

        private static void QuerySvcProcess()
        {
            IEnumerable<Process> processList =
                from p in Process.GetProcesses()
                where String.Equals(p.ProcessName, "svchost")
                orderby p.WorkingSet64 descending
                select p;


            Console.WriteLine("Listing the largest memory processes in SVCHOST: Windows Services");
            foreach (var p in processList)
            {
                Console.WriteLine($"Bytes: {p.PeakWorkingSet64}");
            }
        }

        private static void QueryEmployees()
        {
            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee {ID = 1, Name = "Sean", HireDate = new DateTime(2012, 06, 26)},
                new Employee {ID = 2, Name = "Chris", HireDate = new DateTime(2014, 07, 06)},
                new Employee {ID = 3, Name = "Jim", HireDate = new DateTime(2016, 08, 11)}
            };

            var queryMonth =
                from e in employees
                where e.HireDate.Month == 06
                select e;

            Console.WriteLine("Employees Names who Started in the month of June");
            foreach (var q in queryMonth)
            {
                Console.WriteLine($"Name: {q.Name}");
            }

            var queryYear =
                from e in employees
                where e.HireDate.Year < 2016
                select e;

            Console.WriteLine("Employees Names who Started before the end 2016");
            foreach (var q in queryYear)
            {
                Console.WriteLine($"Name: {q.Name}");
            }
        }
    }
}