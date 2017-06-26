using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot();
            var caretaker = new Caretaker();

            var command = "";

            Console.WriteLine("Robot (X = {0}, Y = {1})", robot.X, robot.Y);

            while(command != "q")
            {
                Console.Write("Enter command (up|down|left|right|undo): ");
                command = Console.ReadLine().Trim();

                switch(command)
                {
                    case "up": // go up
                        caretaker.AddMemento(robot.CreateMemento());
                        robot.Y++;                        
                        break;
                    case "down": // go down
                        caretaker.AddMemento(robot.CreateMemento());
                        robot.Y--;
                        break;
                    case "left": // go left
                        caretaker.AddMemento(robot.CreateMemento());
                        robot.X--;
                        break;
                    case "right": // go right
                        caretaker.AddMemento(robot.CreateMemento());
                        robot.X++;
                        break;
                    case "undo":
                        robot.SetMemento(caretaker.GetLastMemento());
                        break;
                }

                Console.WriteLine("X = {0}, Y = {1}", robot.X, robot.Y);
            }
        }
    }

    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Caretaker
    {
        private Stack<Memento> _mementoes;

        public Caretaker()
        {
            _mementoes = new Stack<Memento>();
        }

        public void AddMemento(Memento m)
        {
            _mementoes.Push(m);
        }

        public Memento GetLastMemento()
        {
            if (_mementoes.Count == 0)
                throw new Exception("no more memento");

            return _mementoes.Pop();
        }
    }

    class Memento
    {
        private Position _pos;

        public Memento(int x, int y)
        {
            _pos = new Position()
            {
                X = x,
                Y = y
            };
        }

        public Position GetState()
        {
            return _pos;
        }
    }

    class Robot
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Memento CreateMemento()
        {
            return new Memento(X, Y);
        }

        public void SetMemento(Memento memento)
        {
            var pos = memento.GetState();
            X = pos.X;
            Y = pos.Y;
        }
    }
}
