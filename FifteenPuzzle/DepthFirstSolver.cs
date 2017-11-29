using System;
using System.Collections.Generic;
using System.Text;

namespace FifteenPuzzle
{
    class DepthFirstSolver : IFifteenPuzzleSolver
    {
        private const int MaxDepth = 3;
        public string Solution { get; private set; }

        public DepthFirstSolver()
        {
            Solution = "";
        }
        public bool Solve(State state, int depth = 1)
        {
            if (state.IsSolved()) return true;
            if (depth > MaxDepth) return false;
            var moves = state.GetMoves();

            for(int i=0; i < moves.Count; i++)
            {
                if(Solve(moves[i], depth + 1))
                {
                    Solution = State.Order[i] + Solution;
                    return true;
                }
            }

            return false;
        }
    }
}
