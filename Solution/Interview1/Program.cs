using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview1
{
    class Program
    {
        static void Main(string[] args)
        {
            int empNameGroups = CountEmployeesByName(new string[] { "Name1", "Name2", "NoName3" });

            Console.WriteLine("Unique Employee Names Found: " + empNameGroups);
            Console.ReadLine();
        }

        // Returns the number of unique employees that have names that match 
        // one of the namesToSearch. 
        static int CountEmployeesByName(string[] namesToSearch)
        {
            using (InterviewEntities context = new InterviewEntities())
            {
                var queries = new List<IEnumerable<employee>>();
                string nameParam;

                foreach (string name in namesToSearch)
                {
                    nameParam = name;

                    //search for employees by name 
                    queries.Add(from e in context.employees
                                where e.name == nameParam
                                select e);
                }

                return queries.Sum(q => q.Count());
            }
        }
        /*Database creation script (make corrections here too): 
        * 
        * CREATE TABLE dbo.employees 
        * ( 
        * uniqueEmployeesId int NOT NULL IDENTITY (1, 1), 
        * name varchar(10) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL, 
        * CONSTRAINT PK_employees PRIMARY KEY CLUSTERED ( 
        * name 
        * ) 
        * ) 
        */ 
    }
}
