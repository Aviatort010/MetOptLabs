﻿using System.Collections.Generic;
using System.Globalization;
using System;

namespace MathUtils
{
    public delegate double FunctionND(Vector x);
    public class Vector : TemplateVector<double>, IEquatable<Vector>
    {
        /// <summary>
        /// Длина вектра
        /// </summary>
        public double MagnitudeSqr => this.Reduce((accum, value) => accum += value * value);

        /// <summary>
        /// Длина вектра
        /// </summary>
        public double Magnitude => Math.Sqrt(MagnitudeSqr);

        /// <summary>
        /// Нормированый вариант вектора
        /// </summary>
        public Vector Normalized
        {
            get
            {
                Vector normalized = new Vector(this);
                double inv_mag = 1.0 / Magnitude;
                normalized.Apply(v => v * inv_mag);
                return normalized;
            }
        }

        /// <summary>
        /// Нормализация вектора
        /// </summary>
        /// <returns></returns>
        public Vector Normalize()
        {
            double inv_mag = 1.0 / Magnitude;
            return new Vector(this[0] * inv_mag, this[1] * inv_mag);
        }

        /// <summary>
        /// Скалярное произведение (this;other)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>(this;other)</returns>
        public double Dot(Vector other)
        {
            if (Count != other.Count) throw new Exception("Unable vector dot multiply");
            return this.Combine(other, (l, r) => l * r).Reduce((acc, v) => acc + v);
        }

        /// <summary>
        /// Скалярное произведение (a;b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>(a;b)</returns>
        public static double Dot(Vector left, Vector right) => left.Dot(right);

        /// <summary>
        /// Строковое представление вектора
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{{{string.Join(", ", this.Map(v => v.ToString("0.000", CultureInfo.InvariantCulture)))}}}";

        public bool Equals(Vector other) => base.Equals(other);

        /// <summary>
        /// Конструктор вектора из массива
        /// </summary>
        /// <param name="args"></param>
        public Vector() : base() { }

        /// <summary>
        /// Конструктор вектора из массива
        /// </summary>
        /// <param name="args"></param>
        public Vector(params double[] args) : base(args) { }

        /// <summary>
        /// Конструктор вектора по размеру и элементу по умолчанию
        /// </summary>
        /// <param name="cap"></param>
        public Vector(int cap) : base(cap) { }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="vect"></param>
        public Vector(Vector other) : base(other) { }
        public Vector(IEnumerable<double> other) : base(other) { }

        /// <summary>
        /// Элементарные математические операции над векторами
        /// </summary>

        ///////////////////////////
        /////    Operator +   /////
        ///////////////////////////
        public static Vector operator +(Vector left, Vector right)
        {
            if (left.Count != right.Count) throw new Exception("error:: operator + :: vectors of different dimensions");

            return new Vector(left.Combine(right, (l, r) => l + r));
        }
        public static Vector operator +(Vector left, double right) => new Vector(left.Combine(right, (l, r) => l + r));
        public static Vector operator +(double left, Vector right) => new Vector(right.Combine(left, (l, r) => r + l));

        ///////////////////////////
        /////    Operator -   /////
        ///////////////////////////
        public static Vector operator -(Vector left, Vector right)
        {
            if (left.Count != right.Count) throw new Exception("error:: operator - :: vectors of different dimensions");
            return new Vector(left.Combine(right, (l, r) => l - r));
        }
        public static Vector operator -(Vector left, double right) => new Vector(left.Combine(right, (l, r) => l - r));
        public static Vector operator -(double left, Vector right) => new Vector(right.Combine(left, (l, r) => r - l));

        ///////////////////////////
        /////    Operator *   /////
        ///////////////////////////
        public static Vector operator *(Vector left, Vector right)
        {
            if (left.Count != right.Count) throw new Exception("error :: operator * :: vectors of different dimensions");
            return new Vector(left.Combine(right, (l, r) => l * r));
        }
        public static Vector operator *(Vector left, double right) => new Vector(left.Combine(right, (l, r) => l * r));
        public static Vector operator *(double left, Vector right) => new Vector(right.Combine(left, (l, r) => r * l));

        ///////////////////////////
        /////    Operator /   /////
        ///////////////////////////
        public static Vector operator /(Vector left, Vector right)
        {
            if (left.Count != right.Count) throw new Exception("error :: operator / :: vectors of different dimensions");
            return new Vector(left.Combine(right, (l, r) => l / r));
        }
        public static Vector operator /(Vector left, double right) => new Vector(left.Combine(right, (l, r) => l / r));
        public static Vector operator /(double left, Vector right) => new Vector(right.Combine(left, (l, r) => r / l));

        /// <summary>
        /// Рассчитывет единичный вектор в направлении от a до b
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector Direction(Vector left, Vector right)
        {
            if (left.Count != right.Count) throw new Exception("error :: dirction :: vectors of different dimensions");
            return (right - left).Normalize();
        }

        /// <summary>
        /// Градиент скалярной функции векторного аргумента 
        /// </summary>
        /// <param name="func">функция для которой рассчитываем градиент</param>
        /// <param name="x">   точка, где рассчитываем градиент</param>
        /// <param name="eps"> шаг центрального разностного аналога</param>
        /// <returns></returns>
        public static Vector Gradient(FunctionND func, Vector x, double eps)
        {
            Vector df = new Vector();
            for (int i = 0; i < x.Count; i++)
                df.PushBack(Partial(func, x, i, eps));
            return df;
        }
        public static Vector Gradient(FunctionND func, Vector x) => Gradient(func, x, NumericCommon.NUMERIC_ACCURACY_MIDDLE);

        /// <summary>
        /// Частная производная в точке x вдоль координаты coord_index
        /// </summary>
        /// <param name="func"></param>
        /// <param name="x"></param>
        /// <param name="coord"></param>
        /// <param name="eps"></param>
        /// <returns></returns>
        public static double Partial(FunctionND func, Vector x, int coord_index, double eps)
        {
            if (x.NotInRange(coord_index)) throw new Exception("Partial derivative index out of bounds!");
            x[coord_index] += eps;
            double f_r = func(x);
            x[coord_index] -= (2.0 * eps);
            double f_l = func(x);
            x[coord_index] += eps;
            return (f_r - f_l) / eps * 0.5;
        }

        public static double Partial(FunctionND func, Vector x, int coord_index) => Partial(func, x, coord_index, NumericCommon.NUMERIC_ACCURACY_MIDDLE);

        public static double Partial2(FunctionND func, Vector x, int coord_index_1, int coord_index_2, double eps)
        {
            if (x.NotInRange(coord_index_2)) throw new Exception("Partial derivative index out of bounds!");
            x[coord_index_2] -= eps;
            double f_l = Partial(func, x, coord_index_1, eps);
            x[coord_index_2] += (2 * eps);
            double f_r = Partial(func, x, coord_index_1, eps);
            x[coord_index_2] -= eps;
            return (f_r - f_l) / eps * 0.5;
        }

        public static double Partial2(FunctionND func, Vector x, int coord_index_1, int coord_index_2) => Partial2(func, x, coord_index_1, coord_index_2, NumericCommon.NUMERIC_ACCURACY_MIDDLE);

        public static double Distance(Vector lhs, Vector rhs)
        {
            return Math.Sqrt(Reduce(lhs.Combine(rhs, (l, r) => (l - r)), (accum, value) => accum + value * value, 0.0));
        }
    }
}
