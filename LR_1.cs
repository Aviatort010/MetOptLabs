using System;


namespace MainSpace
{
    /// <summary>
    /// Класс, обслуживающий первую лабораторную.
    /// </summary>
    class LabOne
    {
        static void swap<T>(ref T lhs, ref T rhs)
        {
            T tmp = lhs;
            lhs = rhs;
            rhs = tmp;
        }
        // Образец внешней функции (чемодан), для передачи её в методы класса.
        public delegate double SomeOneDimFunc(double x);

        /// <summary>
        /// Метод половинного деления.
        /// </summary>
        public static double HalfDevide(
            SomeOneDimFunc func,
            double leftBorder,
            double rightBorder,
            double accuracy,
            int maxIterations = 1000)
        {  
            
            // Если левая граница, на самом деле не левая -- меняем их местами
            if (rightBorder < leftBorder) swap(ref leftBorder, ref  rightBorder);

            int iteration = 0;  // Итерационная переменная
            double xc;// = (rightBorder + leftBorder) / 2.0; // x_center
            for (; iteration <= maxIterations; ++iteration) // сам итератор
            {
                //               xc
                //   |___________|___________|
                // left                    right
                //    \_________/
                //      accuracy
                if (Math.Abs(rightBorder - leftBorder)  < 2.0 * accuracy) break;

                xc = (rightBorder + leftBorder) / 2.0;

                // Здесь мы вызываем функцию по два раза за каждый цикл.
                if (func(xc - accuracy) < func(xc + accuracy))
                    rightBorder = xc;
                else
                    leftBorder = xc;
            }
#if DEBUG
            Console.Write($" bisect:: arg range {rightBorder - leftBorder}\n");
            Console.Write($" bisect:: func probes {iteration * 2}\n");
#endif
            return (rightBorder + leftBorder) * 0.5;
        }

        /// <summary>
        /// Одномерный метод золого сечения.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="leftBorder"></param>
        /// <param name="rightBorder"></param>
        /// <param name="accuracy"></param>
        /// <param name="maxIterations"></param>
        /// <returns></returns>
        public static double GoldenRatio(
            SomeOneDimFunc func,
            double leftBorder,
            double rightBorder,
            double accuracy,
            int maxIterations = 1000)
        {
            if (rightBorder < leftBorder) swap(ref rightBorder, ref leftBorder);

            // Коэффициент золотого сечения (где-то 1.614).
            double phi = (1 + Math.Sqrt(5)) / 2;
            double psi = 1 / phi;

            // Берём пару точек на функции при помощи "Золота"
            // и снова на их сравнении запускаем итератор.
            // Не забываем, что "приёмную" функцию здесь ещё два раза прогоняем.
            double x1 = rightBorder - psi * (rightBorder - leftBorder);
            double fx1 = func(x1);

            double x2 = leftBorder + psi * (rightBorder - leftBorder);
            double fx2 = func(x2);

            int i = 0;
            for (; i < maxIterations; ++i)
            {
                if (rightBorder - leftBorder < 2.0 * accuracy) break;

                if (fx1 > fx2)
                {
                    leftBorder = x1; // Происходит "сдвиг" влево, оттуда, где больше к минимуму.
                    x1 = x2;
                    fx1 = fx2;
                    x2 = leftBorder + psi * (rightBorder - leftBorder);
                    fx2 = func(x2);
                } else
                {
                    rightBorder = x2;
                    x2 = x1;
                    fx2 = fx1;
                    x1 = rightBorder - psi * (rightBorder - leftBorder);
                    fx1 = func(x1);
                }
            }
#if DEBUG
            Console.Write($" GoldenRatio:: arg range {rightBorder - leftBorder}\n");
            Console.Write($" GoldenRatio:: func probes {i + 2}\n");
#endif
            return (rightBorder + leftBorder) * 0.5;
        }

        /// <summary>
        /// Метод Фибоначчи для одномерных функций.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="leftBorder"></param>
        /// <param name="rightBorder"></param>
        /// <param name="accuracy"></param>
        /// <returns></returns>
        public static double Fibonacci(
            SomeOneDimFunc func,
            double leftBorder,
            double rightBorder,
            double accuracy)
        {
            if (rightBorder < leftBorder) swap(ref rightBorder, ref leftBorder);


            double x1, x2;
            double func_1, func_2;
            double val = (rightBorder - leftBorder) / accuracy;

            int i = 0;
            double fib_1 = 1.0, fib_2 = 1.0;
            double fibBuff;
            while (fib_2 < val)     // Через цикл получаем нужное количество чисел Фибоначчи.
            {                       // В основном итераторе будем "идти по ним обратно", к началу.
                fibBuff = fib_1;    // Идея в том, чтобы сдвигаться от больших отрезков к меньшим, 
                fib_1 = fib_2;      // посредством ещё одной последовательности чисел, как в "Золотом"
                fib_2 += fibBuff;
                i++;
            }

            // Пересчёт новых точек по новым, ещё более меньшим числам.
            x1 = leftBorder + (rightBorder - leftBorder) * ((fib_2 - fib_1) / fib_2);
            x2 = leftBorder + (rightBorder - leftBorder) * (fib_1 / fib_2);

            func_1 = func(x1);
            func_2 = func(x2);

            fibBuff = fib_2 - fib_1;
            fib_2 = fib_1;
            fib_1 = fibBuff;

            int i_buff = i;     // Запомним сколько было изначально итераций (чисел)
            for (; i != 0; i--)
            {
                if (func_1 > func_2)
                {                       // Почти тот же алгоритм, как и для "Золотого"
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
                fibBuff = fib_2 - fib_1;    // Расчёт "предыдущих" чисел в последовательности.
                fib_2 = fib_1;
                fib_1 = fibBuff;
            }
#if DEBUG
            Console.Write($" Fibonacci:: arg range {rightBorder - leftBorder}\n");
            Console.Write($" Fibonacci:: func probes {i_buff}\n");
#endif
            return (rightBorder + leftBorder) * 0.5;
        }
    } 
}
