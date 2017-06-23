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
        private static readonly string MEMENTO_FILE_PATH = "memento.txt";

        static void Main(string[] args)
        {
            var robot = new Robot();

            Memento savedMemento = new Memento(0, 0);
            if(savedMemento.LoadFromFile(MEMENTO_FILE_PATH))
            {
                Console.WriteLine("Loaded old state from {0}", MEMENTO_FILE_PATH);
                robot.SetMemento(savedMemento);
            }

            Console.WriteLine("(Initialized State) X = {0}, Y = {1}", robot.X, robot.Y);

            var command = "";

            while(command != "q")
            {
                Console.Write("Enter command (u|d|l|r): ");
                command = Console.ReadLine().Trim();

                switch(command)
                {
                    case "u": // go up
                        robot.Y++;
                        break;
                    case "d": // go down
                        robot.Y--;
                        break;
                    case "l": // go left
                        robot.X--;
                        break;
                    case "r": // go right
                        robot.X++;
                        break;
                }

                Console.WriteLine("X = {0}, Y = {1}", robot.X, robot.Y);
            }

            var memento = robot.CreateMemento();
            memento.SaveToFile(MEMENTO_FILE_PATH);
            Console.WriteLine("Saved state to {0}", MEMENTO_FILE_PATH);
        }
    }

    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
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

        public void SaveToFile(string path)
        {
            File.WriteAllText(path, string.Format("{0}\n{1}\n", _pos.X, _pos.Y));
        }

        public bool LoadFromFile(string path)
        {
            if (File.Exists(path))
            {
                var pos = File.ReadAllLines(path);
                _pos = new Position()
                {
                    X = int.Parse(pos[0]),
                    Y = int.Parse(pos[1])
                };

                return true;
            }
            else
            {
                return false;
            }
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
