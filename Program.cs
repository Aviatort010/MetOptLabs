using System;


namespace MainSpace
{
    class Program
    {
        /// <summary>
        /// Одномерная Функция для испытаний алгоритмов
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double Func(double x)
        {
            return (x + 3) * (x - 5);
        }


        static void Main(string[] args)
        {
            Console.WriteLine(" \tОдномерные:");
            double left = -10, right = 10,
                acc = 1e-5;

            Console.WriteLine("    Границы: [ " + left + "; " + right + " ],");
            Console.WriteLine("    Точность: " + acc);

            double HDresult = LabOne.HalfDevide(Func, left, right, acc);
            Console.Write(" Половинное деление - \t");
            Console.WriteLine("( " + HDresult.ToString("##.###") + " )\n");

            double GRresult = LabOne.GoldenRatio(Func, left, right, 2 * acc);
            Console.Write(" Золотое сечение    - \t");
            Console.WriteLine("( " + GRresult.ToString("##.###") + " )\n");

            double FBresult = LabOne.Fibonacci(Func, left, right, acc);
            Console.Write(" Метод Фибоначчи    - \t");
            Console.WriteLine("( " + FBresult.ToString("##.###") + " )\n");

            Console.ReadKey();
        }
    }
}
