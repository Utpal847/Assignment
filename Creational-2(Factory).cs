using System;

namespace FactoryPatternDemo
{
    public abstract class Shape
    {
        public abstract void Draw();
    }

    public class Circle : Shape
    {
        public override void Draw() => Console.WriteLine("Drawing Circle");
    }

    public class Square : Shape
    {
        public override void Draw() => Console.WriteLine("Drawing Square");
    }

    public static class ShapeFactory
    {
        public static Shape CreateShape(string shapeType)
        {
            return shapeType.ToLower() switch
            {
                "circle" => new Circle(),
                "square" => new Square(),
                _ => throw new ArgumentException("Unknown shape type")
            };
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Creational-2 (Factory)");
            var shape1 = ShapeFactory.CreateShape("circle");
            var shape2 = ShapeFactory.CreateShape("square");

            shape1.Draw();
            shape2.Draw();
        }
    }
}
