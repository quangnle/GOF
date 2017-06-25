using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorPattern
{
    class Program
    {
        /// <summary>
        /// A collection item
        /// </summary>
        class Employee
        {
            private string _name;

            public Employee(string name)
            {
                this._name = name;
            }

            public string Name
            {
                get { return _name; }
            }
        }

        /// <summary>
        /// The 'Aggregate' interface
        /// </summary>
        interface IAbstractCollection
        {
            Iterator CreateIterator();
        }

        /// <summary>
        /// The 'ConcreteAggregate' class
        /// </summary>
        class Collection : IAbstractCollection
        {
            private ArrayList _items = new ArrayList();

            public Iterator CreateIterator()
            {
                return new Iterator(this);
            }

            // Lấy số lượng các phần tử
            public int Count
            {
                get { return _items.Count; }
            }

            // Indexer
            public object this[int index]
            {
                get { return _items[index]; }
                set { _items.Add(value); }
            }
        }

        /// <summary>
        /// The 'Iterator' interface
        /// </summary>
        interface IAbstractIterator
        {
            Employee Current();
            Employee Next();
            bool HasNext { get; }
        }

        /// <summary>
        /// The 'ConcreteIterator' class
        /// </summary>
        class Iterator : IAbstractIterator
        {
            private Collection _collection;
            private int _current = 0;

            public Iterator(Collection collection)
            {
                this._collection = collection;
            }

            public Employee Next()
            {
                if (HasNext)
                {
                    _current++;
                    return _collection[_current] as Employee;
                }
                else
                    return null;
            }

            public Employee Current()
            {
                return _collection[_current] as Employee;
            }

            public bool HasNext
            {
                get
                {
                    return (_current + 1) < _collection.Count;
                }
            }
        }

        static void Main(string[] args)
        {
            // Build a collection
            Collection collection = new Collection();
            collection[0] = new Employee("Tran Quang A");
            collection[1] = new Employee("Le Nhat B");
            collection[2] = new Employee("Le Van C");
            collection[3] = new Employee("Tran Van D");
            collection[4] = new Employee("Tao Nhu C");

            // Create iterator
            Iterator iterator = collection.CreateIterator();

            Console.WriteLine("Iterating over collection:");

            // Get current Employee
            var itemCurrent = iterator.Current();
            while (iterator.HasNext)
            {
                var item = iterator.Next();
                Console.WriteLine(item.Name);
            }

            Console.ReadKey();

        }
    }
}
