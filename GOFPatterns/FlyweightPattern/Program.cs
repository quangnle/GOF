using System;
using System.Collections;

namespace FlyweightPattern
{
    class Client
    {
        static void Main(string[] args)
        {
            var factory = new BlockBuilderFactory();

            var starBuilder = factory.GetPattern("*");
            starBuilder.Display(5, 10);

            var sharpBuilder = factory.GetPattern("#");
            sharpBuilder.Display(15, 10);

            Console.ReadLine();
        }
    }

    public class BlockBuilderFactory //FlyweightFactory 
    {
        private Hashtable _builders = new Hashtable();

        // Constructor
        public BlockBuilderFactory()
        {
            _builders.Add("*", new StarBlock());
            _builders.Add("#", new SharpBlock());
        }

        public BlockBuilder GetPattern(string key) // get flyweight
        {
            return ((BlockBuilder)_builders[key]);
        }
    }

    public abstract class BlockBuilder // Flyweight
    {
        public abstract void Display(int height, int width);
    }

    public class StarBlock : BlockBuilder // Concrete Flyweight
    {
        public override void Display(int height, int width)
        {
            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }

    public class SharpBlock : BlockBuilder // Concrete Flyweight
    {
        public override void Display(int height, int width)
        {
            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }

        }
    }
}
