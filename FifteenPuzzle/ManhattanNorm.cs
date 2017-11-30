using System;
using System.Collections.Generic;
using System.Text;

namespace FifteenPuzzle
{
    class ManhattanNorm : INorm
    {
        public int GetNorm(State state)
        {
            int norm = state.Depth;

            for (int i = 0; i < State.SizeX; i++)
                for (int j = 0; j < State.SizeY; j++)
                {
                    if (state.Current[i, j] == 0) continue;
                    norm += Math.Abs((state.Current[i, j] - 1) / State.SizeX - i);
                    norm += Math.Abs((state.Current[i, j] - 1) % State.SizeX - j);
                }

            return norm;
        }
    }
}
