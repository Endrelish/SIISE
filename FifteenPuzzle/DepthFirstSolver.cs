using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FifteenPuzzle
{
    class DepthFirstSolver : IFifteenPuzzleSolver
    {
        private readonly int maxDepth = 25;
        public int MaxDepth { get { return maxDepth; }}
        public string Solution { get; private set; }
        public int StatesChecked { get; private set; }
        public int StatesProcessed { get; private set; }
        public float Time { get; private set; }
        public bool IsSolved { get; private set; }
        private List<State> list;
        private HashSet<State> checkedStates;

        public DepthFirstSolver()
        {
            Solution = "";
        }

        public void Solve(State state)
        {
            list = new List<State>();
            checkedStates = new HashSet<State>(new StateEqualityComparer());
            StatesChecked = 0;
            StatesProcessed = 1;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            IsSolved = SolveIfSolvable(state);
            stopwatch.Stop();

            Time = stopwatch.ElapsedMilliseconds / 1000f;
        }
        private bool SolveIfSolvable(State state)
        {
            State currentState;
            list.Add(state);
            StatesChecked++;
            if (state.IsSolved()) return true;
            while(list.Count > 0)
            {
                currentState = list[0];
                list.RemoveAt(0);
                //checkedStates.Add(currentState);

                if(currentState.IsSolved())
                {
                    while(currentState.Parent != null)
                    {
                        Solution = currentState.GetMove() + Solution;
                        currentState = currentState.Parent;
                    }
                    StatesChecked = checkedStates.Count;
                    return true;
                }
                else
                {
                    List<State> newStates = new List<State>();
                    foreach(State item in currentState.GetMoves())
                    {
                        if (/*!checkedStates.Contains(item) && */item.Depth < maxDepth)
                        {
                            newStates.Add(item);
                            StatesProcessed++;
                        }
                    }
                    list.InsertRange(0, newStates);
                }
            }

            return false;
        }
    }
}
