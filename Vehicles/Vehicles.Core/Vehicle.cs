using System;
using System.Collections.Generic;

namespace Vehicles.Core
{
    public interface IDrivable
    {
        string Drive(int steps);
    }

    public interface ISwimmable
    {
        string Swim(int steps);
    }

    public abstract class Vehicle
    {
        public string Name { get; set; }
        public int Mileage { get; protected set; }
        public List<string> Log { get; } = new List<string>();
        public string VehicleType => GetType().Name;

        protected Vehicle(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Unnamed Vehicle" : name;
            Log.Add($"[{DateTime.Now:HH:mm:ss}] Created vehicle '{Name}'.");
        }

        public abstract void Move(int steps);

        protected bool ValidateSteps(int steps)
        {
            if (steps <= 0)
            {
                Log.Add($"[{DateTime.Now:HH:mm:ss}] Error ({Name}): Invalid move attempt ({steps} steps). State unchanged.");
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

        public string Drive(int steps) => $"{Name} (Car) drove {steps} steps on land.";

        public override void Move(int steps)
        {
            if (!ValidateSteps(steps)) return;

            Mileage += steps;
            string message = $"[{DateTime.Now:HH:mm:ss}] " + Drive(steps);
            Log.Add(message);
            Console.WriteLine(message);
        }
    }

    public class Boat : Vehicle, ISwimmable
    {
        public Boat(string name) : base(name) { }

        public string Swim(int steps) => $"{Name} (Boat) swam {steps} steps in water.";

        public override void Move(int steps)
        {
            if (!ValidateSteps(steps)) return;

            Mileage += steps;
            string message = $"[{DateTime.Now:HH:mm:ss}] " + Swim(steps);
            Log.Add(message);
            Console.WriteLine(message);
        }
    }

    public class AmphibiousCar : Vehicle, IDrivable, ISwimmable
    {
        public AmphibiousCar(string name) : base(name) { }

        public string Drive(int steps) => $"{Name} (AmphibiousCar) drove {steps} steps on land.";
        public string Swim(int steps) => $"{Name} (AmphibiousCar) swam {steps} steps through water.";

        public override void Move(int steps)
        {
            if (!ValidateSteps(steps)) return;

            Mileage += steps;
            string message = $"[{DateTime.Now:HH:mm:ss}] {Drive(steps / 2)} and {Swim(steps - steps / 2)}";
            Log.Add(message);
            Console.WriteLine(message);
        }
    }
}