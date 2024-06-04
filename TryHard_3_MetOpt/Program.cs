using System.Collections.Generic;
using System;
using MathUtils;


namespace MainSpace
{
    class Program
    {

        static double Testf2(Vector args)
        {
            return (args[0] - 2.0) * (args[0] - 2.0) + (args[1] - 2.0) * (args[1] - 2.0);
        }

        /// Пример применения функций штрафа
        /// Уловие 1
        static double Psi1(Vector args)
        {
            return 1.0 / (5.0 - args[0] * 2.0 + args[1] * 3.0);
        }
        /// Уловие 1
        static double Psi2(Vector args)
        {
            return 1.0 / (6.0 + args[0] * 3.0 - args[1]);
        }
        ///Ишем минимум функции  Testf2 при условии Psi1 и Psi2(Это внутренний штраф)
        static double Func(Vector args)
        {
            return Testf2(args) + Psi2(args) + Psi1(args);
        }

        static void Lab_1_test(
            LabOne.SomeOneDimFunc testFunction,
            double left = -10,
            double right = 10,
            double acc = 1e-5)
        {
            Console.WriteLine(" \tОдномерные:");

            Console.WriteLine("    Границы: [ " + left + "; " + right + " ],");
            Console.WriteLine("    Точность: " + acc);

            double HDresult = LabOne.HalfDevide(testFunction, left, right, acc);
            Console.Write(" Половинное деление - \t");
            Console.WriteLine("( " + HDresult.ToString("##.###") + " )\n");

            double GRresult = LabOne.GoldenRatio(testFunction, left, right, 2 * acc);
            Console.Write(" Золотое сечение    - \t");
            Console.WriteLine("( " + GRresult.ToString("##.###") + " )\n");

            double FBresult = LabOne.Fibonacci(testFunction, left, right, acc);
            Console.Write(" Метод Фибоначчи    - \t");
            Console.WriteLine("( " + FBresult.ToString("##.###") + " )\n");

            Console.ReadKey();
        }

        static void Lab_2_test(
            FunctionND testFunction,
            Vector left,
            Vector right,
            double acc = 1e-5)
        {
            Console.WriteLine($" \tМногомерные:");

            Console.WriteLine($"    Границы (Векторы):\n\t Слева = {left}, Справа = {right}");
            Console.WriteLine($"    Точность: {acc}\n");

            Vector HDresult = LabTwo.HalfDevide(testFunction, left, right, acc);
            Console.Write($" Половинное деление (Бисекции) ......\t");
            Console.WriteLine($" {HDresult} \n");

            Vector GRresult = LabTwo.GoldenRatio(testFunction, left, right, acc);
            Console.Write($" Золотое сечение ....................\t");
            Console.WriteLine($" {GRresult} \n");

            Vector FBresult = LabTwo.Fibonacci(testFunction, left, right, acc);
            Console.Write($" Фибоначи ...........................\t");
            Console.WriteLine($" {FBresult} \n");

            Console.Write($" Покоординатный спуск ...............\t");
            Console.WriteLine($" {LabTwo.CoordinateDescent(testFunction, left)} ");

            Console.ReadKey();
        }


        static void Lab_3_test(
            FunctionND testFunction,
            Vector left,
            Vector right,
            double acc = 1e-5)
        {
            Console.WriteLine($" \tМногомерные с Градиентами:");

            Console.WriteLine($"    Границы (Векторы):\n\t Слева = {left}, Справа = {right}");
            Console.WriteLine($"    Точность: {acc}\n");

            Vector GDresult = LabThree.GradientDescent(testFunction, left, acc);
            Console.Write($" Градиентный спуск (Катящийся мячик) ...\t");
            Console.WriteLine($" {GDresult} \n");

            Vector GJGresult = LabThree.GonjubasGradients(testFunction, left, acc);
            Console.Write($" Совмещённый Градиентный спуск .........\t");
            Console.WriteLine($" {GJGresult} \n");

            Vector NRFresult = LabThree.Newtone_Raphson(testFunction, left, acc);
            Console.Write($" Ньютон-Рафсон .........................\t");
            Console.WriteLine($" {NRFresult} \n");

            Console.ReadKey();
        }


        ////////////////////
        /// Lab. work #4 ///
        ////////////////////
        static void Lab4()
        {
            Console.WriteLine("\n////////////////////\n");
            Console.WriteLine("/// Lab. work #4 ///\n");
            Console.WriteLine("////////////////////\n\n");
            Vector x_1 = new Vector(0.0, 0.0);
            Vector x_0 = new Vector(5.0, 5.0);
            Console.WriteLine($"x_0 = {x_0}, x_1 = {x_1}\n");
            Console.WriteLine($"NewtoneRaphson         : {LabThree.Newtone_Raphson(Testf2, x_1)}");
            Console.WriteLine($"NewtoneRaphson         : {LabThree.Newtone_Raphson(Func, x_1)}\n");
        }

        public static void SimpexTest()
        {



            Console.WriteLine("\n/////////////////////////////");
            Console.WriteLine("//////// SimplexTest ////////");
            Console.WriteLine("/////////////////////////////\n");

            Vector b = new Vector(40.0, 28.0, 14.0);
            Vector c = new Vector(2.0, 3.0);

            Matrix A = new Matrix
            (
              new Vector(-2.0, 6.0),
              new Vector(3.0, 2.0),
              new Vector(2.0, -1.0)
            );

            Console.WriteLine(" f(x,c) =  2x1 + 3x2;\n arg_max = {4, 8}, f(arg_max) = 32");
            Console.WriteLine(" |-2x1 + 6x2 <= 40");
            Console.WriteLine(" | 3x1 + 2x2 <= 28");
            Console.WriteLine(" | 2x1 -  x2 <= 14\n");

            Simplex sym_0 = new Simplex(A, c, new List<Sign>() { Sign.Less, Sign.Less, Sign.Less }, b);
            sym_0.Solve(SimplexProblemType.Max);

            Console.WriteLine("\n f(x,c) = -2x1 + 3x2;\n arg_min = {7, 0}, f(arg_min) =-14\n");

            Simplex sym_1 = new Simplex(A, new Vector(-2.0, 3.0), new List<Sign>() { Sign.Less, Sign.Less, Sign.Less }, b);
            sym_1.Solve(SimplexProblemType.Min);


            Console.WriteLine("/////////////////////////////");
            Console.WriteLine(" f(x,c) =  2x1 + 3x2;\n arg_min = {62/5, 54/5}, f(arg_max) = 57 1/5");
            Console.WriteLine(" |-2x1 + 6x2 >= 40");
            Console.WriteLine(" | 3x1 + 2x2 >= 28");
            Console.WriteLine(" | 2x1 -  x2 >= 14\n");
            Simplex sym_2 = new Simplex(A, new Vector(2.0, 1.0), new List<Sign>() { Sign.More, Sign.More, Sign.More }, b);
            sym_2.Solve(SimplexProblemType.Min);
            Console.WriteLine(" f(x,c) =  -2x1 - x2;\n arg_min = {62/5, 54/5}, f(arg_max) = -35 3/5");

            Simplex sym_3 = new Simplex(A, new Vector(-2.0, -1.0), new List<Sign>() { Sign.More, Sign.More, Sign.More }, b);
            sym_3.Solve(SimplexProblemType.Max);
            Console.WriteLine(" f(x,c) =  2x1 + 3x2;\n arg_min = {none, none}, f(arg_max) = none");
            Simplex sym_4 = new Simplex(A, c, new List<Sign>() { Sign.Equal, Sign.Equal, Sign.Equal }, b);
            sym_4.Solve(SimplexProblemType.Max);

            Console.ReadLine();
        }


        /// <summary>
        /// Одномерная Функция для испытаний алгоритмов
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double Func(double x)
        {
            return (x + 3) * (x - 5);
        }

        static double MultiDimFunc(Vector xv)
        {
            return (xv[0] - 2.0) * (xv[0] - 2.0) + (xv[1] - 2.0) * (xv[1] - 2.0);
        }

        static void Main(string[] args)
        {
            double acc = 1e-5;
            /*
            double left = -10;
            double right = 10;

            Lab_1_test(Func, left, right, acc);
            */

            Vector leftVector = new Vector(0.0, 0.0);
            Vector rightVector = new Vector(5.0, 5.0);

            //Lab_2_test(MultiDimFunc, leftVector, rightVector, acc);

            //Lab_3_test(MultiDimFunc, leftVector, rightVector, acc);

            SimpexTest();
        }
    }
}