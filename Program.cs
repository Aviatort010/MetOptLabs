using System;


namespace MainSpace
{
    class Program
    {
        static double Func(double x)
        {
            return x * (x - 5);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(" \tОдномерные:");
            double left = -10, right = 10, acc = 0.000001;

            Console.WriteLine("    Границы: [ " + left + "; " + right + " ],");
            Console.WriteLine("    Точность: " + acc);

            (double, int) HDresult = LabOne.HalfDevide(Func, left, right, acc);
            (double, int) GRresult = LabOne.GoldenRatio(Func, left, right, acc);
            (double, int) FBresult = LabOne.Fibonacci(Func, left, right, acc);

            Console.Write(" Половинное деление - \t");
            Console.WriteLine("( " + HDresult.Item1.ToString("##.###") + "; " + HDresult.Item2.ToString() + " )");
            Console.Write(" Золотое сечение    - \t");
            Console.WriteLine("( " + GRresult.Item1.ToString("##.###") + "; " + GRresult.Item2.ToString() + " )");
            Console.Write(" Метод Фибоначчи    - \t");
            Console.WriteLine("( " + FBresult.Item1.ToString("##.###") + "; " + FBresult.Item2.ToString() + " )");


            Console.ReadKey();
        }
    }
}
