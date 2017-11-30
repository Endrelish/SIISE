using System;
using System.Collections.Generic;
using System.Text;

namespace FifteenPuzzle
{
    interface IFifteenPuzzleSolver
    {
        string Solution { get; }
        int StatesChecked { get; }
        int StatesProcessed { get; }        // Nie wiem co to jest, trzeba ogarnac =>
        int MaxDepth { get; }
        float Time { get; }
        bool IsSolved { get; }
        void Solve(State state);
    }
}
