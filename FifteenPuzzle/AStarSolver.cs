using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FifteenPuzzle
{
    class AStarSolver : IFifteenPuzzleSolver
    {
        public int MaxDepth { get; private set; }
        public string Solution { get; private set; }
        public int StatesChecked { get; private set; }
        public int StatesProcessed { get; private set; }
        public float Time { get; private set; }
        public bool IsSolved { get; private set; }
        private static INorm norm;
        private SortedList<int, State> list;
        private HashSet<State> checkedStates;

        public static void SetNorm(string option)
        {
            switch(option)
            {
                case "hamm":
                    norm = new HammingNorm();
                    break;
                case "manh":
                    norm = new ManhattanNorm();
                    break;
            }
        }


        private bool SolveIfSolvable(State state)
        {
            list.Add(norm.GetNorm(state), state);

            while(list.Count > 0)
            {
                var currentState = list.Values[0];
                list.RemoveAt(0);
                StatesProcessed++;
                checkedStates.Add(currentState);

                if (currentState.IsSolved())
                {
                    while (currentState.Parent != null)
                    {
                        Solution = currentState.GetMove() + Solution;
                        currentState = currentState.Parent;
                    }
                    StatesChecked = checkedStates.Count;
                    GetDepth();
                    StatesProcessed += list.Count;
                    return true;
                }
                else
                {
                    foreach(State item in currentState.GetMoves())
                    {
                        if(!checkedStates.Contains(item))
                            list.Add(norm.GetNorm(item), item);
                    }
                }
            }

            return false;
        }

        private void GetDepth()
        {
            int count = StatesChecked;
            MaxDepth = 0;
            while (count > 0)
            {
                MaxDepth++;
                count >>= 2;
            }
        }

        

        public void Solve(State state)
        {
            list = new SortedList<int, State>(new DuplicateComparer<int>());
            checkedStates = new HashSet<State>(new StateEqualityComparer());
            Solution = "";
            StatesChecked = 0;
            StatesProcessed = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IsSolved = SolveIfSolvable(state);
            stopwatch.Stop();
            Time = stopwatch.ElapsedMilliseconds / 1000f;
        }
    }
}
