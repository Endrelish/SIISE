using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FifteenPuzzle
{
    class BreadthFirstSolver : IFifteenPuzzleSolver
    {
        public int MaxDepth { get; private set; }
        public string Solution { get; private set; }
        public int StatesChecked { get; private set; }
        public int StatesProcessed { get; private set; }
        public float Time { get; private set; }
        public bool IsSolved { get; private set; }
        private SortedList<int, State> list;
        private HashSet<State> checkedStates;

        public void Solve(State state)
        {
            int depth = 1;
            State currentState;
            StatesProcessed = 1;
            list = new SortedList<int, State>(new DuplicateComparer<int>());
            checkedStates = new HashSet<State>(new StateEqualityComparer());
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            list.Add(1, state);

            while(list.Count > 0)
            {
                if (list.Keys[0] == list.Keys[list.Count - 1])
                    GetChildren(depth + 1);

                currentState = list.Values[0];
                depth = list.Keys[0];
                list.RemoveAt(0);
                checkedStates.Add(currentState);

                if(currentState.IsSolved())
                {
                    while (currentState.Parent != null)
                    {
                        Solution = currentState.GetMove() + Solution;
                        currentState = currentState.Parent;
                    }
                    stopwatch.Stop();
                    IsSolved = true;
                    Time = stopwatch.ElapsedMilliseconds / 1000f;
                    MaxDepth = depth;
                    StatesChecked = checkedStates.Count;
                    return;
                }
            }
            stopwatch.Stop();
            IsSolved = false;
            Time = stopwatch.ElapsedMilliseconds / 1000f;
            return;
        }

        private void GetChildren(int depth)
        {
            SortedList<int, State> newElements = new SortedList<int, State>(new DuplicateComparer<int>());
            foreach (var item in list)
            {
                foreach (var state in item.Value.GetMoves())
                {
                    StatesProcessed++;
                    if(!checkedStates.Contains(state))
                        newElements.Add(depth, state);
                }
            }

            foreach (var state in newElements)
                list.Add(state.Key, state.Value);
            
        }
    }
}
