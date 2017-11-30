using System;
using System.Collections.Generic;
using System.Text;

namespace FifteenPuzzle
{
    class HammingNorm : INorm
    {
        public int GetNorm(State state)
        {
            int norm = 0;
            for(int i = 0; i < State.SizeX; i++)
                for(int j = 0; j < State.SizeY; j++)
                    if (state.Current[i, j] != State.Target[i, j])
                        norm++;

            return norm;
        }
    }
}
