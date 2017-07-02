using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            // người dùng cuối chỉ cần biết dùng ChartDrawer
            // không cần quan tâm nó làm gì bên trong
            ChartDrawer cd = new ChartDrawer();
            cd.Draw("data.csv");
            Console.ReadLine();
        }
    }

    public class ChartDrawer // facade
    {
        public void Draw(string file)
        {
            var employees = CSVLoader.Load(file);

            Drawer drawer = new Drawer();
            drawer.Display(employees);
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
    }

    public class LineParser  //  subsystem
    {
        public static Employee Parse(string line, string delimiter)
        {
            var employee = new Employee();
            var employeeInfo = line.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            return new Employee()
            {
                Name = employeeInfo[0],
                Salary = int.Parse(employeeInfo[1])
            };
        }
    }

    public class CSVLoader //  subsystem
    {
        public static List<Employee> Load(string file)
        {
            var list = new List<Employee>();

            var lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                var item = LineParser.Parse(line, ",");
                list.Add(item);
            }

            return list;
        }
    }

    public class Drawer //  subsystem
    {
        private void DrawOne(Employee employee)
        {
            var template = "{0,-20} {1}";
            var salary = "";
            for (var i = 0; i < (employee.Salary); i++)
            {
                salary = salary + "#";
            }

            Console.WriteLine(string.Format(template, employee.Name, salary));
        }

        public void Display(List<Employee> employees)
        {
            foreach (Employee emp in employees)
            {
                DrawOne(emp);
            }
        }
    }
}
