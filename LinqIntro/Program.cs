using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqIntro
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee {ID = 1, Name = "Sean", HireDate = new DateTime(2016, 06, 26)},
                new Employee {ID = 2, Name = "Chris", HireDate = new DateTime(2016, 07, 06)},
                new Employee {ID = 3, Name = "Jim", HireDate = new DateTime(2016, 08, 11)}
            };

            var query =
                from e in employees
                where e.HireDate.Month == 06
                select e;

            foreach (var q in query)
            {
                Console.WriteLine($"Name: {q.Name}");
            }
       }
    }
}