using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FifteenPuzzle
{
    class DepthFirstSolver : IFifteenPuzzleSolver
    {
        private readonly int maxDepth = 3;
        public int MaxDepth { get { return MaxDepth; }}
        public string Solution { get; private set; }
        public int StatesChecked { get; private set; }
        public int StatesProcessed { get; private set; }
        public float Time { get; private set; }
        public bool IsSolved { get; private set; }

        public DepthFirstSolver()
        {
            Solution = "";
        }

        public void Solve(State state)
        {
            StatesChecked = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            IsSolved = Solve(state, 1);
            stopwatch.Stop();

            Time = stopwatch.ElapsedMilliseconds / 1000f;
        }
        public bool Solve(State state, int depth)
        {
            StatesChecked++;
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
