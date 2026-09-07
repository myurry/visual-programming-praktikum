namespace PizzaSplit.Core
{
    public static class BillCalculator
    {
        public static int ExampleMethod(int a, int b)
        {
            return a + b;
        }

        public static float EvaluateBill(float costPerPerson, int n, bool tip) 
        {
            float total = costPerPerson * n;
            if (tip)
            {
                total += (float)(total * 0.1);
            }
            return (float)Math.Round(total, 2);
        }
    }
}
