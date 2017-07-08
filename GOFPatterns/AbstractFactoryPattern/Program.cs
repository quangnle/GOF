using System;

public class AbstractFactoryPattern
{
    public static void Main(String[] args)
    {
        Menu menu;

        // if Saturday or Sunday then use Thai Menu Set, otherwise Viet 
        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            menu = new ThaiMenu();
        else
            menu = new VietMenu();

        Console.WriteLine("Menu for today");
        Console.WriteLine("Appetizer: {0}", menu.CreateAppetizer().Name);
        Console.WriteLine("Main course: {0}", menu.CreateMainCourse().Name);
        Console.WriteLine("Dessert: {0}", menu.CreateDessert().Name);
    }

    public abstract class Menu
    {
        public abstract IAppetizer CreateAppetizer();
        public abstract IMainCourse CreateMainCourse();
        public abstract IDessert CreateDessert();
    }

    public class VietMenu : Menu // concrete factory
    {
        public override IAppetizer CreateAppetizer() { return new SpringRoll(); }
        public override IMainCourse CreateMainCourse() { return new Hotpot(); }
        public override IDessert CreateDessert() { return new Fruit(); }
    }

    public class ThaiMenu : Menu // concrete factory
    {
        public override IAppetizer CreateAppetizer() { return new TomyumGung(); }
        public override IMainCourse CreateMainCourse() { return new PadThai(); }
        public override IDessert CreateDessert() { return new MangoStickyRice(); }
    }

    public interface IAppetizer
    {
        string Name { get; }
    }

    public interface IMainCourse
    {
        string Name { get; }
    }

    public interface IDessert
    {
        string Name { get; }
    }

    public class SpringRoll : IAppetizer // product
    {
        public string Name { get { return "Spring Roll"; } }
    }

    public class Hotpot : IMainCourse // product
    {
        public string Name { get { return "Hotpot"; } }
    }

    public class Fruit : IDessert // product
    {
        public string Name { get { return "Fruit"; } }
    }

    public class TomyumGung : IAppetizer // product
    {
        public string Name { get { return "Tomyum Gung"; } }
    }

    public class PadThai : IMainCourse // product
    {
        public string Name { get { return "PadThai"; } }
    }

    public class MangoStickyRice : IDessert // product
    {
        public string Name { get { return "Mango Sticky Rice"; } }
    }
}