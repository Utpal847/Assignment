using System;

namespace StrategyPatternDemo
{
    public interface IPaymentStrategy
    {
        void Pay(int amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(int amount) =>
            Console.WriteLine($"Paid {amount} using Credit Card");
    }

    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(int amount) =>
            Console.WriteLine($"Paid {amount} using PayPal");
    }

    public class ShoppingCart
    {
        private IPaymentStrategy strategy;

        public void SetPaymentStrategy(IPaymentStrategy strategy) => this.strategy = strategy;

        public void Checkout(int amount)
        {
            if (strategy != null) strategy.Pay(amount);
            else Console.WriteLine("No payment strategy selected");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Behavioural-2 (Strategy)");
            var cart = new ShoppingCart();
            cart.SetPaymentStrategy(new CreditCardPayment());
            cart.Checkout(100);
            cart.SetPaymentStrategy(new PayPalPayment());
            cart.Checkout(200);
        }
    }
}
