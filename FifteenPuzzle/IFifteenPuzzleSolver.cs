using System;
using System.Collections.Generic;
using System.Text;

namespace FifteenPuzzle
{
    interface IFifteenPuzzleSolver
    {
        string Solution { get; }
        bool Solve(State state, int depth = 1);
    }
}
