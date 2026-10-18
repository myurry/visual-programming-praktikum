using System;
using System.Collections.Generic;

namespace Vehicles.Core
{
    public interface IDrivable
    {
        void Drive(int steps);
    }

    public interface ISwimmable
    {
        void Swim(int steps);
    }

    public abstract class Vehicle
    {
        public string Name { get; set; }
        public int Mileage { get; protected set; }
        public List<string> Log { get; } = new List<string>();
        public string VehicleType => GetType().Name;

        public bool IsDrivable => this is IDrivable;
        public bool IsSwimmable => this is ISwimmable;

        protected Vehicle(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Unnamed Vehicle" : name;
            Log.Add($"[{DateTime.Now:HH:mm:ss}] Created vehicle '{Name}'.");
        }

        public void LogMessage(string message)
        {
            Log.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        public abstract void Move(int steps);

        protected bool ValidateSteps(int steps)
        {
            if (steps <= 0)
            {
                LogMessage($"Error ({Name}): Invalid movement attempt ({steps} steps). State unchanged.");
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"{Name} ({GetType().Name}) — Mileage: {Mileage} steps";
        }
    }

    public class Car : Vehicle, IDrivable
    {
        public Car(string name) : base(name) { }

        public void Drive(int steps)
        {
            if (!ValidateSteps(steps)) return;

            Mileage += steps;
            string message = $"[{DateTime.Now:HH:mm:ss}] {Name} (Car) drove {steps} steps on land.";
            Log.Add(message);
            Console.WriteLine(message);
        }

        public override void Move(int steps) => Drive(steps);
    }

    public class Boat : Vehicle, ISwimmable
    {
        public Boat(string name) : base(name) { }

        public void Swim(int steps)
        {
            if (!ValidateSteps(steps)) return;

            Mileage += steps;
            string message = $"[{DateTime.Now:HH:mm:ss}] {Name} (Boat) swam {steps} steps in water.";
            Log.Add(message);
            Console.WriteLine(message);
        }

        public override void Move(int steps) => Swim(steps);
    }

    public class AmphibiousCar : Vehicle, IDrivable, ISwimmable
    {
        public AmphibiousCar(string name) : base(name) { }

        public void Drive(int steps)
        {
            if (!ValidateSteps(steps)) return;

            Mileage += steps;
            string message = $"[{DateTime.Now:HH:mm:ss}] {Name} (AmphibiousCar) drove {steps} steps on land.";
            Log.Add(message);
            Console.WriteLine(message);
        }

        public void Swim(int steps)
        {
            if (!ValidateSteps(steps)) return;

            Mileage += steps;
            string message = $"[{DateTime.Now:HH:mm:ss}] {Name} (AmphibiousCar) swam {steps} steps through water.";
            Log.Add(message);
            Console.WriteLine(message);
        }

        public override void Move(int steps)
        {
            if (!ValidateSteps(steps)) return;

            // General Move triggers a combined amphibious journey
            Drive(steps / 2);
            Swim(steps - steps / 2);
        }
    }
}