using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CarBuilder homeCarBuilder = new HomeCarBuilder();
            CarBuilder sportCarBuilder = new SportCarBuilder();

            Director director = new Director();

            director.Construct(homeCarBuilder);
            var homeCar = homeCarBuilder.GetResult();
            Console.WriteLine(homeCar.Specifications);

            director.Construct(sportCarBuilder);
            var sportCar = sportCarBuilder.GetResult();
            Console.WriteLine(sportCar.Specifications);
        }
    }

    public class Director
    {
        public void Construct(CarBuilder builder)
        {
            builder.BuildBody();
            builder.BuildEngine();
            builder.BuildWheels();
        }
    }

    public abstract class CarBuilder // abstract builder
    {
        public abstract void BuildBody();
        public abstract void BuildEngine();
        public abstract void BuildWheels();

        public abstract Car GetResult();
    }

    public class HomeCarBuilder : CarBuilder // concrete builder
    {
        private Car _car = new Car();

        public override void BuildBody()
        {
            _car.Body = "Five Seat Body";
        }

        public override void BuildEngine()
        {
            _car.Engine = "1.8 Horse Power Engine";
        }

        public override void BuildWheels()
        {
            _car.Wheels = "Steel wheels";
        }

        public override Car GetResult()
        {
            return _car;
        }
    }

    public class SportCarBuilder : CarBuilder // concrete builder
    {
        private Car _car = new Car();

        public override void BuildBody()
        {
            _car.Body = "Two Seat Body";
        }

        public override void BuildEngine()
        {
            _car.Engine = "3.5 Horse Power Engine";
        }

        public override void BuildWheels()
        {
            _car.Wheels = "Alloy wheels";
        }

        public override Car GetResult()
        {
            return _car;
        }
    }

    public class Car // product
    {
        public string Body { get; set; }
        public string Engine { get; set; }
        public string Wheels { get; set; }

        public string Specifications
        {
            get
            {
                return string.Format("Body: {0} - Engine: {1} - Wheels: {2}", Body, Engine, Wheels);
            }
        }
    }
}
