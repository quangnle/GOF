using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPattern
{
    class Program
    {
        // Client
        static void Main(string[] args)
        {
            var bot = new Robot();
            var controller = new RobotController(bot);

            // create and set commands to receiver
            controller.AddCommand(new MoveCommand());
            controller.AddCommand(new MoveCommand());
            controller.AddCommand(new TurnRightCommand());
            controller.AddCommand(new MoveCommand());
            controller.AddCommand(new TurnLeftCommand());
            controller.AddCommand(new MoveCommand());
            controller.AddCommand(new TurnLeftCommand());
            controller.AddCommand(new MoveCommand());

            controller.Execute();
        }
    }

    enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        NumDirections
    }

    class Robot
    {
        public Robot()
        {
            Dir = Direction.Up;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Direction Dir { get; set; }
    }

    // command definitions
    interface ICommand
    {
        void Execute(Robot bot);
    }

    class TurnRightCommand : ICommand
    {
        public void Execute(Robot bot)
        {
            Console.WriteLine("Executing TurnRight command");
            bot.Dir = (Direction)(((int)bot.Dir + 1) % (int)Direction.NumDirections);
        }
    }

    class TurnLeftCommand : ICommand
    {
        public void Execute(Robot bot)
        {
            Console.WriteLine("Executing TurnLeft command");
            bot.Dir = (Direction)((((int)bot.Dir + (int)Direction.NumDirections) - 1) % (int)Direction.NumDirections);
        }
    }

    class MoveCommand : ICommand
    {
        public void Execute(Robot bot)
        {
            Console.WriteLine("Executing Move command");

            switch (bot.Dir)
            {
                case Direction.Up:
                    bot.Y++;
                    break;
                case Direction.Down:
                    bot.Y--;
                    break;
                case Direction.Left:
                    bot.X--;
                    break;
                case Direction.Right:
                    bot.X++;
                    break;
            }
        }
    }

    // Receiver and Invoker
    class RobotController
    {
        private readonly Robot _bot;
        private readonly Queue<ICommand> _commands;

        public RobotController(Robot bot)
        {
            _bot = bot;
            _commands = new Queue<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            _commands.Enqueue(command);
        }

        public void Execute()
        {
            Console.WriteLine("[Robot] Position: ({0}, {1}), Direction: {2}", _bot.X, _bot.Y, _bot.Dir.ToString());

            while (_commands.Count > 0)
            {
                var command = _commands.Dequeue();
                command.Execute(_bot);

                Console.WriteLine("Position: ({0}, {1}), Direction: {2}", _bot.X, _bot.Y, _bot.Dir.ToString());
            }
        }
    }
}
