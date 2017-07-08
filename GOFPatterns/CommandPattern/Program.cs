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
            controller.AddCommand(new GoStraightCommand());
            controller.AddCommand(new GoStraightCommand());
            controller.AddCommand(new GoRightCommand());
            controller.AddCommand(new GoBackCommand());
            controller.AddCommand(new GoLeftCommand());
            controller.AddCommand(new GoStraightCommand());
            controller.AddCommand(new GoLeftCommand());

            controller.Execute();
        }
    }

    class Robot
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    // command definitions
    interface ICommand
    {
        void Execute(Robot bot);
    }

    class GoStraightCommand : ICommand
    {
        public void Execute(Robot bot)
        {
            Console.WriteLine("Executing GoStraightCommand");

            bot.Y++;
        }
    }

    class GoBackCommand : ICommand
    {
        public void Execute(Robot bot)
        {
            Console.WriteLine("Executing GoBackCommand");

            bot.Y--;
        }
    }

    class GoLeftCommand : ICommand
    {
        public void Execute(Robot bot)
        {
            Console.WriteLine("Executing GoLeftCommand");

            bot.X--;
        }
    }

    class GoRightCommand : ICommand
    {
        public void Execute(Robot bot)
        {
            Console.WriteLine("Executing GoRightCommand");

            bot.X++;
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
            Console.WriteLine("[Robot] ({0}, {1})", _bot.X, _bot.Y);

            while (_commands.Count > 0)
            {
                var command = _commands.Dequeue();
                command.Execute(_bot);

                Console.WriteLine("Position: ({0}, {1})", _bot.X, _bot.Y);
            }
        }
    }
}
