using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessClass businessClassTiket = new BusinessClass();

            MealDecorator mealDecorator = new MealDecorator(businessClassTiket);
            BaggageDecorator baggageDecorator = new BaggageDecorator(mealDecorator);

            Console.WriteLine(string.Format("{0} total price {1}", baggageDecorator.GetName(), baggageDecorator.GetPrice()));

            Console.ReadKey();

        }
    }

    public abstract class ClassComponent
    {
        public abstract string GetName();
        public abstract double GetPrice();
    }

    public class BusinessClass : ClassComponent
    {
        public override string GetName()
        {
            return "Business class ticket";
        }

        public override double GetPrice()
        {
            return 155;
        }
    }

    public class EconomyClass : ClassComponent
    {
        public override string GetName()
        {
            return "Economy class ticket";
        }

        public override double GetPrice()
        {
            return 100;
        }
    }

    public abstract class Decorator : ClassComponent
    {
        ClassComponent classComponent = null;

        protected string _name = "undefined";
        protected double _price = 0;

        protected Decorator(ClassComponent baseComponent)
        {
            classComponent = baseComponent;
        }

        public override string GetName()
        {
            return string.Format("{0} and {1}", classComponent.GetName(), _name);
        }

        public override double GetPrice()
        {
            return classComponent.GetPrice() + _price;
        }
    }

    /// <summary>
    /// Dịch vụ thức ăn
    /// </summary>
    public class MealDecorator : Decorator
    {
        public MealDecorator(ClassComponent baseComponent) : base(baseComponent)
        {
            this._name = "Meal Service";
            this._price = 15;
        }
    }

    /// <summary>
    /// Dịch vụ bảo hiểm máy bay
    /// </summary>
    public class InsuranceDecorator : Decorator
    {
        public InsuranceDecorator(ClassComponent baseComponent) : base(baseComponent)
        {
            this._name = "Insurance Service";
            this._price = 30;
        }
    }

    /// <summary>
    /// Dịch vụ hành lý
    /// </summary>
    public class BaggageDecorator : Decorator
    {
        public BaggageDecorator(ClassComponent baseComponent) : base(baseComponent)
        {
            this._name = "Baggage Service";
            this._price = 7;
        }
    }
}
