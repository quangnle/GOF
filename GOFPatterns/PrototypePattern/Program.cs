using System;
using System.Collections.Generic;

namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IEmployee> lst = new List<IEmployee>();
            IEmployee sharpLead = new Employee("Ryan", "Leader", "C#");
            IEmployee javaDev = new Employee("John", "SE", "Java");

            lst.Add(sharpLead);
            lst.Add(javaDev);

            Employee sharpCopy = (Employee)sharpLead.Clone();
            sharpCopy.Name = "Harry";

            lst.Add(sharpCopy);

            foreach (var emp in lst)
            {
                Console.WriteLine(emp.GetDetails());
            }

            Console.ReadKey();

        }
    }

    /// <summary>
    /// The 'Prototype' interface
    /// </summary>
    public interface IEmployee
    {
        IEmployee Clone();
        string GetDetails();
    }

    /// <summary>
    /// A 'ConcretePrototype' class
    /// </summary>
    public class Employee : IEmployee
    {
        public int WordsPerMinute { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PreferredLanguage { get; set; }
        public Project Project { get; set; }

        public Employee(string name, string role, string preferredLanguage)
        {
            this.Name = name;
            this.Role = role;
            this.PreferredLanguage = preferredLanguage;
        }

        public IEmployee Clone()
        {
            // Shallow Copy: chỉ clone được value type
            return (IEmployee)MemberwiseClone();

            // TODO: Deep Copy: clone toàn bộ object
            //var emp= (Employee)MemberwiseClone();
            //emp.Project = (Project)Project.Clone();
            //return emp;
        }

        public string GetDetails()
        {
            return string.Format("{0} - {1} - {2}", Name, Role, PreferredLanguage);
        }
    }

    public class Project : ICloneable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            return (Project)MemberwiseClone();
        }
    }
}

