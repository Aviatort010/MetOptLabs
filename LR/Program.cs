using System;


namespace MainSpace
{
    class Program
    {
        static double Func(double x)
        {
            return -x;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Одномерные");
            double left = -10, right = 10, acc = 0.01;

            (double, int) HDresult = LabOne.HalfDevide(Func, left, right, acc);
            (double, int) GRresult = LabOne.GoldenRatio(Func, left, right, acc);
            (double, int) FBresult = LabOne.Fibonacci(Func, left, right, acc);
            Console.WriteLine(HDresult);
            Console.WriteLine(GRresult);
            Console.WriteLine(FBresult);


            Console.WriteLine("Двумерные");

        }
    }
}
