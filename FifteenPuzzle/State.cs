using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FifteenPuzzle
{
    class State
    {
        private static Size puzzleSize = new Size();
        public static int SizeX => puzzleSize.SizeX;
        public static int SizeY => puzzleSize.SizeY;
        public static int [,] Target;
        public static char [] Order = { 'R', 'L', 'U', 'D' };
        public int [,] Current;
        public State Parent { get; }
        public int Depth { get; }

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

        public State(int[,] current, State parent = null, int depth = 1)
        {
            Current = new int[puzzleSize.SizeX, puzzleSize.SizeY];
            Parent = parent;
            Depth = depth;
            Array.Copy(current, Current, puzzleSize.SizeX * puzzleSize.SizeY);
        }

        public char GetMove()
        {
            int posX = 0, posY = 0, parPosX = 0, parPosY = 0;
            for (int i = 0; i < State.SizeX; i++)
                for (int j = 0; j < State.SizeY; j++)
                {
                    if (this.Current[i, j] == 0)
                    {
                        posX = i;
                        posY = j;
                    }
                    if (this.Parent.Current[i, j] == 0)
                    {
                        parPosX = i;
                        parPosY = j;
                    }
                }

            if (posX > parPosX) return 'D';
            if (posX < parPosX) return 'U';
            if (posY > parPosY) return 'R';
            if (posY < parPosY) return 'L';
            return '\0';
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
                    case 'U':
                        if (posX > 0)
                        {
                            State movedState = new State(Current, this);
                            movedState.Current[posX, posY] = movedState.Current[posX - 1, posY];
                            movedState.Current[posX - 1, posY] = 0;
                            moves.Add(movedState);
                        }
                        break;
                    case 'D':
                        if (posX < puzzleSize.SizeX - 1)
                        {
                            State movedState = new State(Current, this);
                            movedState.Current[posX, posY] = movedState.Current[posX + 1, posY];
                            movedState.Current[posX + 1, posY] = 0;
                            moves.Add(movedState);
                        }
                        break;
                    case 'L':
                        if (posY > 0)
                        {
                            State movedState = new State(Current, this);
                            movedState.Current[posX, posY] = movedState.Current[posX, posY - 1];
                            movedState.Current[posX, posY - 1] = 0;
                            moves.Add(movedState);
                        }
                        break;
                    case 'R':
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

        public bool Equals(State other)
        {
            int[,] otherBoard = other.Current;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (!(Current[i, j] == otherBoard[i, j]))
                        return false;
            return true;
        }

        private class Size
        {
            public int SizeX { get; set; }
            public int SizeY { get; set; }
        }
    }
}
