using System;
using System.Collections.Generic;
using System.Text;

namespace FifteenPuzzle
{
    class DuplicateComparer<T> : IComparer<T> where T : IComparable
    {
        public int Compare(T x, T y)
        {
            int result = x.CompareTo(y);

            if (result == 0)
                return 1;   // Handle equality as beeing greater
            else
                return result;
        }
    }
}
