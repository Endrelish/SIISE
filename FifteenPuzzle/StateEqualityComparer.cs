using System;
using System.Collections.Generic;
using System.Text;

namespace FifteenPuzzle
{
    class StateEqualityComparer : IEqualityComparer<State>
    {
        public bool Equals(State x, State y)
        {
            for (int i = 0; i < State.SizeX; i++)
                for (int j = 0; j < State.SizeY; j++)
                    if (x.Current[i, j] != y.Current[i, j]) return false;
            return true;
        }

        public int GetHashCode(State obj)
        {
            int hash = 0;
            foreach(int elem in obj.Current)
            {
                hash += elem;
            }
            return hash;
        }
    }
}
