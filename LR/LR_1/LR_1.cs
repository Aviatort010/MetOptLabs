using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainSpace
{
    /// <summary>
    /// Первая лабораторная.
    /// </summary>
    class LabOne
    {
        // Образец внешней функции (чемодан), для передачи её в методы класса
        public delegate double SomeOneDimFunc(double x);

        /// <summary>
        /// Метод половинчатого деления.
        /// </summary>
        public static (double, int) HalfDevide(
            SomeOneDimFunc func,
            double leftBorder,
            double rightBorder,
            double accuracy,
            int maxIterations = 1000)
        {
            // Если левая граница, на самом деле не левая ;)
            if (rightBorder < leftBorder) (leftBorder, rightBorder) = (rightBorder, leftBorder);

            int i = 0;
            double xc = 0.0; // x_center
            for (; i <= maxIterations; ++i)
            {   // Можно ли здесь убрать Модуль??
                if (Math.Abs(rightBorder - leftBorder) / 2.0 < accuracy) break;

                xc = (rightBorder + leftBorder) / 2.0;
                double middleFuncPoint = func(xc);

                if (func(xc - accuracy) < middleFuncPoint)
                {
                    rightBorder = xc;
                }
                else if (func(xc + accuracy) < middleFuncPoint)
                {
                    leftBorder = xc;
                }
                else
                {
                    leftBorder = xc - accuracy;
                    rightBorder = xc + accuracy;
                }
            }
            return (Math.Round(xc, 3), i);
        }

        public static (double, int) GoldenRatio(
            SomeOneDimFunc func,
            double leftBorder,
            double rightBorder,
            double accuracy,
            int maxIterations = 1000)
        {
            if (rightBorder < leftBorder) (leftBorder, rightBorder) = (rightBorder, leftBorder);

            double phi = (1 + Math.Sqrt(5)) / 2;
            double phi2 = 2 - phi;

            double x1 = leftBorder + phi2 * (rightBorder - leftBorder);
            double fx1 = func(x1);

            double x2 = rightBorder - phi2 * (rightBorder - leftBorder);
            double fx2 = func(x2);

            int i = 0;
            for (; i < maxIterations; ++i)
            {
                if (Math.Abs(rightBorder - leftBorder) / 2.0 <= accuracy) break;

                if (fx1 < fx2)
                {
                    rightBorder = x2;
                    x2 = x1;
                    fx2 = fx1;
                    x1 = leftBorder + phi2 * (rightBorder - leftBorder);
                    fx1 = func(x1);
                    continue;
                }
                leftBorder = x1;
                x1 = x2;
                fx1 = fx2;
                x2 = rightBorder - phi2 * (rightBorder - leftBorder);
                fx2 = func(x2);
            }
            return ((leftBorder + rightBorder) / 2.0, i);
        }


        public static (double, int) Fibonacci(
            SomeOneDimFunc func,
            double leftBorder,
            double rightBorder,
            double accuracy)
        {
            if (rightBorder < leftBorder) (leftBorder, rightBorder) = (rightBorder, leftBorder);

            double x1, x2;
            double func_1, func_2;
            double val = (rightBorder - leftBorder) / accuracy;

            int i = 0;
            double fib_1 = 1.0, fib_2 = 1.0;
            double fibBuff;
            while (fib_2 < val) // Через цикл получаем нужное количество чисел Фибоначчи.
            {                   // В основном итераторе будем "идти обратно" к началу
                fibBuff = fib_1;
                fib_1 = fib_2;
                fib_2 += fibBuff;
                i++;
            }

            x1 = leftBorder + (rightBorder - leftBorder) * ((fib_2 - fib_1) / fib_2);
            x2 = leftBorder + (rightBorder - leftBorder) * (fib_1 / fib_2);

            func_1 = func(x1);
            func_2 = func(x2);

            fibBuff = fib_2 - fib_1;
            fib_2 = fib_1;
            fib_1 = fibBuff;

            int i_buff = i;
            for (; i != 0; i--)
            {
                if (func_1 > func_2)
                {
                    leftBorder = x1;
                    func_1 = func_2;
                    x1 = x2;
                    x2 = leftBorder + (rightBorder - leftBorder) * (fib_1 / fib_2);
                    func_2 = func(x2);
                }
                else
                {
                    rightBorder = x2;
                    x2 = x1;
                    func_2 = func_1;
                    x1 = leftBorder + (rightBorder - leftBorder) * ((fib_2 - fib_1) / fib_2);
                    func_1 = func(x1);
                }
                fibBuff = fib_2 - fib_1;
                fib_2 = fib_1;
                fib_1 = fibBuff;
            }
            return ((leftBorder + rightBorder) / 2, i_buff);
        }
    } 
}
