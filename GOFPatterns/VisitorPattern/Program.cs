using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<Shape>();
            list.Add(new Circle(10));
            list.Add(new Rectangle(5, 5));

            AreaUpdater areaUpdater = new AreaUpdater();
            CircumferenceUpdater circumferenceUpdater = new CircumferenceUpdater();

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Update(areaUpdater); // update area 
                list[i].Update(circumferenceUpdater); // update circumference

                Console.WriteLine("Area = {0}; Circumference = {1}", list[i].Area, list[i].Circumference);
            }

            Console.Read();
        }
    }

    public abstract class ShapeUpdater
    {
        public abstract void UpdateCircle(Circle c);
        public abstract void UpdateRectangle(Rectangle r);
    }

    public class AreaUpdater : ShapeUpdater
    {
        public override void UpdateCircle(Circle c)
        {
            c.Area = c.Radius * c.Radius * Math.PI;
        }

        public override void UpdateRectangle(Rectangle r)
        {
            r.Area = r.Height * r.Width;
        }
    }

    public class CircumferenceUpdater : ShapeUpdater
    {
        public override void UpdateCircle(Circle c)
        {
            c.Circumference = 2 * c.Radius * Math.PI;
        }

        public override void UpdateRectangle(Rectangle r)
        {
            r.Circumference = 2 * (r.Height + r.Width);
        }
    }

    public abstract class Shape
    {
        public double Area { get; set; }
        public double Circumference { get; set; }
        public abstract void Update(ShapeUpdater updater);
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle(double r)
        {
            Radius = r;
        }

        public override void Update(ShapeUpdater updater)
        {
            updater.UpdateCircle(this);
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double w, double h)
        {
            Width = w;
            Height = h;
        }

        public override void Update(ShapeUpdater updater)
        {
            updater.UpdateRectangle(this);
        }
    }
}
