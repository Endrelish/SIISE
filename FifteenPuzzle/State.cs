using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FifteenPuzzle
{
    class State
    {
        private static Size puzzleSize = new Size();
        public static int [,] Target;
        public static char [] Order = { 'R', 'L', 'U', 'D' };
        public int [,] Current;
        public State Parent;

        public bool IsSolved()
        {
            for (int i = 0; i < puzzleSize.SizeX; i++)
                for (int j = 0; j < puzzleSize.SizeY; j++)
                    if (Current[i, j] != Target[i, j]) return false;
            return true;
        }

        public static void SetPuzzleSize(int sizeX, int sizeY)
        {
            puzzleSize.SizeX = sizeX;
            puzzleSize.SizeY = sizeY;
            
            Target = new int[sizeX, sizeY];
            for(int i = 0; i < sizeX; i++)
                for(int j = 0; j < sizeY; j++)
                {
                    Target[i, j] = i * sizeX + j + 1;
                }
            Target[sizeX - 1, sizeY - 1] = 0;
        }

        public static void SetSearchOrder(string order)
        {
            char[] orderArray = order.ToCharArray();
            Order = orderArray;
            for(int i=0; i<4; i++)
            {
                Order[i] = char.ToUpper(Order[i]);
            }
        }

        public State(int[,] current, State parent = null)
        {
            Current = new int[puzzleSize.SizeX, puzzleSize.SizeY];
            Parent = parent;
            Array.Copy(current, Current, puzzleSize.SizeX * puzzleSize.SizeY);
        }
        

        public List<State> GetMoves()
        {
            List<State> moves = new List<State>();
            int posX = -1, posY = -1;
            bool found = false;
            for (int i = 0; i < puzzleSize.SizeX; i++)
            {
                for (int j = 0; j < puzzleSize.SizeY; j++)
                {
                    if (Current[i, j] == 0)
                    {
                        posX = i;
                        posY = j;
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }

            if (!found) return null;

            for(int i = 0; i < 4; i++)
            {
                switch(Order[i])
                {
                    case 'L':
                        if (posX > 0)
                        {
                            State movedState = new State(Current, this);
                            movedState.Current[posX, posY] = movedState.Current[posX - 1, posY];
                            movedState.Current[posX - 1, posY] = 0;
                            moves.Add(movedState);
                        }
                        break;
                    case 'R':
                        if (posX < puzzleSize.SizeX - 1)
                        {
                            State movedState = new State(Current, this);
                            movedState.Current[posX, posY] = movedState.Current[posX + 1, posY];
                            movedState.Current[posX + 1, posY] = 0;
                            moves.Add(movedState);
                        }
                        break;
                    case 'U':
                        if (posY > 0)
                        {
                            State movedState = new State(Current, this);
                            movedState.Current[posX, posY] = movedState.Current[posX, posY - 1];
                            movedState.Current[posX, posY - 1] = 0;
                            moves.Add(movedState);
                        }
                        break;
                    case 'D':
                        if (posY < puzzleSize.SizeY - 1)
                        {
                            State movedState = new State(Current, this);
                            movedState.Current[posX, posY] = movedState.Current[posX, posY + 1];
                            movedState.Current[posX, posY + 1] = 0;
                            moves.Add(movedState);
                        }
                        break;
                }
            }

            return moves;
        }

        private class Size
        {
            public int SizeX { get; set; }
            public int SizeY { get; set; }
        }
    }
}
