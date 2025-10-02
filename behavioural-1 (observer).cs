using System;
using System.Collections.Generic;

namespace ObserverPatternDemo
{
    public interface IObserver
    {
        void Update(int temperature);
    }

    public class WeatherStation
    {
        private readonly List<IObserver> observers = new();
        private int temperature;

        public void AddObserver(IObserver observer) => observers.Add(observer);
        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        public void SetTemperature(int temp)
        {
            temperature = temp;
            Notify();
        }

        private void Notify()
        {
            foreach (var obs in observers)
            {
                obs.Update(temperature);
            }
        }
    }

    public class PhoneDisplay : IObserver
    {
        public void Update(int temperature) =>
            Console.WriteLine($"Phone Display: Temperature updated to {temperature}C");
    }

    public class WindowDisplay : IObserver
    {
        public void Update(int temperature) =>
            Console.WriteLine($"Window Display: Temperature updated to {temperature}C");
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Observer Pattern-1");
            var station = new WeatherStation();
            var phone = new PhoneDisplay();
            var window = new WindowDisplay();

            station.AddObserver(phone);
            station.AddObserver(window);

            station.SetTemperature(25);
        }
    }
}
