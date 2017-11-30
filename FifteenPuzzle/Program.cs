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

            try { beginningState = GetBeginningState(); }
            catch { return; }

            puzzleSolver.Solve(beginningState);

            try
            {
                SaveSolution();
                SaveDetails();
            }
            catch { return; }

            Console.WriteLine("okeoke");
            Console.ReadKey();

        }

        private static void SaveDetails()
        {
            try
            {
                using (StreamWriter outputStream = new StreamWriter(additionalOutputFile))
                {
                    if(puzzleSolver.IsSolved) outputStream.WriteLine(puzzleSolver.Solution.Length);
                    else outputStream.WriteLine("-1");
                    outputStream.WriteLine(puzzleSolver.StatesChecked);
                    outputStream.WriteLine(puzzleSolver.StatesProcessed);
                    outputStream.WriteLine(puzzleSolver.MaxDepth);
                    outputStream.WriteLine(puzzleSolver.Time);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Solution details could not be saved to file {0}", additionalOutputFile);
                Console.ReadKey();
                throw e;
            }
        }

        private static void SaveSolution()
        {
            string solution = puzzleSolver.Solution;
            try
            {
                using (StreamWriter outputStream = new StreamWriter(outputFile))
                {
                    if (puzzleSolver.IsSolved)
                    {
                        outputStream.WriteLine(solution.Length);
                        outputStream.WriteLine(solution);
                    }
                    else
                    {
                        outputStream.WriteLine("-1");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Solution could not be saved to file {0}.", outputFile);
                Console.ReadKey();
                throw e;
            }
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
                Console.ReadKey();
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
                    AStarSolver.SetNorm(option);
                    break;
                default:
                    Console.WriteLine("Wrong strategy parameter.");
                    Console.ReadKey();
                    return false;
            }
            return true;
        }

        private static bool SetParameters(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Wrong number of arguments.");
                Console.ReadKey();
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
