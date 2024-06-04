// Индиана Иванович - В поисках затерянного Slice.
using System;

namespace MathUtils
{
    // sealed означает, грубо говоря, финальный, терминальный (не возможность наследования класса)
    public sealed class Slice
    {
        private readonly int _end;
        private readonly int _begin;
        private readonly int _step;

        //exclusive index
        public int End()
        {
            return _end;
        }

        //inclusive index
        public int Begin()
        {
            return _begin;
        }

        public int Step()
        {
            return _step;
        }

        public Slice()
        {
            _begin = 0;
            _end = 0;
            _step = 1;
        }

        public Slice(int begin, int end) : this(begin, end, 1)
        {
        }

        public Slice(int begin, int end, int step)
        {
            _step = step; // == 0 ? 1 : step;
            _begin = begin;
            _end = end;
        }

        public int Length
        {
            get
            {
                // Calculate the number of elements in the slice
                return (Math.Abs(_end - _begin) + Math.Abs(_step) - 1) / Math.Abs(_step);
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            return Equals((Slice)obj);
        }

        public bool Equals(Slice slice)
        {
            if (slice.Begin() != Begin()) return false;
            if (slice.End() != End()) return false;
            if (slice.Step() != Step()) return false;
            return true;
        }

        public override string ToString()
        {
            return Step() == 1 ? $"{Begin()}:{End()}" : $"{Begin()}:{End()}:{Step()}";
        }

        public override int GetHashCode()
        {
            return (Begin().GetHashCode() |( End().GetHashCode()) |  Step().GetHashCode());
        }
    }
}
