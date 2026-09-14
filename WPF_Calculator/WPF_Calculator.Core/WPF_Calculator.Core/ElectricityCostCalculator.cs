namespace WPF_Calculator.Core
{
    public static class ElectricityCostCalculator
    {
        public static int ExampleMethod(int a, int b)
        {
            return a + b;
        }

        public static double EvaluateCost(double powerWatts, double hoursPerDay, double pricePerKwh)
        {
            double kwhPerDay = (powerWatts * hoursPerDay) / 1000.0;
            double totalCost = kwhPerDay * 30.0 * pricePerKwh;

            return totalCost;
        }
    }
}
