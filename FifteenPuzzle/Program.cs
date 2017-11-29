using System;
using System.IO;

namespace FifteenPuzzle
{
    class Program
    {
        private static IFifteenPuzzleSolver puzzleSolver;

        private static string strategy;
        private static string option;
        private static string inputFile;
        private static string outputFile;
        private static string additionalOutputFile;

        static void Main(string[] args)
        {
            if (!SetParameters(args)) return;
            if (!SetPuzzleSolver()) return;
            State beginningState = new State(new int [0, 0]);
            try
            {
                beginningState = GetBeginningState();
            }
            catch
            {
                return;
            }

            if(puzzleSolver.Solve(beginningState))
            {
                Console.WriteLine(puzzleSolver.Solution.Length);
                Console.WriteLine(puzzleSolver.Solution);
            }
            else
            {
                Console.WriteLine("Solution not found.");
            }

            Console.WriteLine("okeoke");

        }

        private static State GetBeginningState()
        {
            try
            {
                using (StreamReader inputStream = new StreamReader(inputFile))
                {
                    string line = inputStream.ReadLine();
                    int [] size = GetIntegers(line, 2);
                    int sizeX = size[0];
                    int sizeY = size[1];

                    int[,] board = new int[sizeX, sizeY];
                    int[] row;

                    for(int i = 0; i < sizeX; i++)
                    {
                        line = inputStream.ReadLine();
                        row = GetIntegers(line, sizeY);
                        for(int j = 0; j < sizeY; j++)
                        {
                            board[i, j] = row[j];
                        }
                    }

                    State.SetPuzzleSize(sizeX, sizeY);
                    State beginningState = new State(board);

                    return beginningState;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The input file could not be read.");
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        
        private static int[] GetIntegers(string line, int numberOfIntegers)
        {
            int [] integers = new int[numberOfIntegers];

            while(!int.TryParse(line.Substring(0,1), out int x))
            {
                line = line.Substring(1);
            }

            for(int i=0; i < numberOfIntegers; i++)
            {
                if(line.Contains(" "))
                    integers[i] = int.Parse(line.Substring(0, line.IndexOf(' ')));
                else
                    integers[i] = int.Parse(line);
                line = line.Substring(line.IndexOf(' ') + 1);
            }

            return integers;
        }

        private static bool SetPuzzleSolver()
        {
            switch(strategy)
            {
                case "bfs":
                    puzzleSolver = new BreadthFirstSolver();
                    State.SetSearchOrder(option);
                    break;
                case "dfs":
                    puzzleSolver = new DepthFirstSolver();
                    State.SetSearchOrder(option);
                    break;
                case "astr":
                    puzzleSolver = new AStarSolver();
                    break;
                default:
                    Console.WriteLine("Wrong strategy parameter.");
                    return false;
            }
            return true;
        }

        private static bool SetParameters(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Wrong number of arguments.");
                return false;
            }
            strategy = args[0];
            option = args[1];
            inputFile = args[2];
            outputFile = args[3];
            additionalOutputFile = args[4];

            return true;
        }
    }
}
