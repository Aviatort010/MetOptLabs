using System;
using MathUtils;


namespace MainSpace
{
    class LabThree
    {
        //-----------------------------------{Градиентный спуск}-----------------------------------
        public static Vector GradientDescent (
            FunctionND func,
            Vector startDot,
            double accuracy = 1e-5,
            int maxIterations = 1000)
        {
            Vector dot_2 = startDot;

            int i = 0;

            for (; i <= maxIterations; ++i)
            {
                dot_2 = startDot - Vector.Gradient(func, startDot, accuracy);
                dot_2 = LabTwo.Fibonacci(func, startDot, dot_2, accuracy);

                if (Vector.Distance(dot_2, startDot) < accuracy)
                    break;

                startDot = dot_2;
            }
#if DEBUG
            Console.Write($" Gradient Descent::  iterations {i}\n");
#endif
            return (startDot + dot_2) * 0.5;
        }


        //-----------------------------------{Сопряжённо-Градиентный спуск}-----------------------------------
        public static Vector GonjubasGradients(
            FunctionND func,
            Vector startDot,
            double accuracy = 1e-5,
            int maxIterations = 1000)
        {
            Vector dot_2 = startDot;

            // Вычислили направление движения
            Vector sot_1 = (-1.0) * Vector.Gradient(func, startDot, accuracy);
            Vector sot_2;

            double omega;
            int i = 0;
            for (; i <= maxIterations; ++i)
            {
                // Сдвинулись в новую точку
                dot_2 = startDot + sot_1;
                // Подравнялись
                dot_2 = LabTwo.Fibonacci(func, startDot, dot_2, accuracy);

                if (Vector.Distance(dot_2, startDot) < accuracy)
                    break;

                sot_2 = Vector.Gradient(func, dot_2, accuracy);

                omega = Math.Pow((sot_2).Magnitude, 2) / Math.Pow((sot_1).Magnitude, 2);
                sot_1 = sot_1 * omega - sot_2;

                startDot = dot_2;
            }


#if DEBUG
            Console.Write($" Conj Gradients::  iterations {i}\n");
#endif
            return (startDot + dot_2) * 0.5;
        }


        //-----------------------------------{Ньютон-Раффсон}-----------------------------------
        public static Vector Newtone_Raphson(
            FunctionND func,
            Vector startDot,
            double accuracy = 1e-5,
            int maxIterations = 1000)
        {
            Vector dot_2 = startDot;

            int i = 0;
            for (; i <= maxIterations; ++i)
            {
                dot_2 = startDot - Matrix.Invert(Matrix.Hessian(func, startDot, accuracy)) * Vector.Gradient(func, startDot, accuracy);
                
                if (Vector.Distance(dot_2, startDot) < accuracy)
                    break;

                startDot = dot_2;
            }
#if DEBUG
            Console.Write($" Newtone-Raphson::  iterations {i}\n");
#endif
            return (dot_2 + startDot) * 0.5;
        }
    }
}