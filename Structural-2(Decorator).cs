using System;

namespace DecoratorPatternDemo
{
    public abstract class Coffee
    {
        public abstract int Cost();
    }

    public class SimpleCoffee : Coffee
    {
        public override int Cost() => 5;
    }

    public abstract class CoffeeDecorator : Coffee
    {
        protected Coffee coffee;
        public CoffeeDecorator(Coffee coffee) => this.coffee = coffee;
    }

    public class Milk : CoffeeDecorator
    {
        public Milk(Coffee coffee) : base(coffee) { }
        public override int Cost() => coffee.Cost() + 2;
    }

    public class Sugar : CoffeeDecorator
    {
        public Sugar(Coffee coffee) : base(coffee) { }
        public override int Cost() => coffee.Cost() + 1;
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Structural-2 (Decorator)");
            Coffee coffee = new SimpleCoffee();
            Console.WriteLine($"Base Coffee cost: {coffee.Cost()}");

            coffee = new Milk(coffee);
            Console.WriteLine($"Coffee + Milk cost: {coffee.Cost()}");

            coffee = new Sugar(coffee);
            Console.WriteLine($"Coffee + Milk + Sugar cost: {coffee.Cost()}");
        }
    }
}
