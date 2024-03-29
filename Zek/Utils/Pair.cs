﻿using System.Text;

namespace Zek.Utils
{
    public class Pair<TFirst, TSecond>
    {
        public TFirst First { get; set; }

        public TSecond Second { get; set; }

        public Pair()
        {
        }
        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }
        public Pair<TFirst, TSecond> Clone()
        {
            return new Pair<TFirst, TSecond>(First, Second);
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (First != null)
            {
                builder.Append(First);
            }
            builder.Append(", ");
            if (Second != null)
            {
                builder.Append(Second);
            }
            builder.Append(']');
            return builder.ToString();
        }
        public override bool Equals(object obj)
        {
            var pair = obj as Pair<TFirst, TSecond>;
            if (pair == null)
                return false;
            return Equals(First, pair.First) && Equals(Second, pair.Second);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
