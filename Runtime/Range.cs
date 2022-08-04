using System;

namespace Pospec.Helper
{
    [Serializable]
    public class Range<T> where T : IComparable
    {
        public T Min { get; set; }
        public T Max { get; set; }

        public Range(T min, T max)
        {
            if (min.CompareTo(max) < 0)
                throw new ArgumentException("Min value is bigger than Max value");

            Min = min;
            Max = max;
        }

        public bool InRange(T value)
        {
            return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
        }

        public bool InsideRange(T value)
        {
            return value.CompareTo(Min) > 0 && value.CompareTo(Max) < 0;
        }
    }
}
