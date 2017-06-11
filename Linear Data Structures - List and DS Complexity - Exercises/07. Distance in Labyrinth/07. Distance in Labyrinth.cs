using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _07.DistanceInLabyrinth
{
    class Program
    {
        private static string[,] labyrinth;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            ReadLabyrinth(n);
            List<KeyValuePair<int, int>> cells = GetCells();
            KeyValuePair<int, int> startPoint = FindStart(cells);
            WalkIt(startPoint, cells);
            PrintLabyrinth();
        }

        private static void ReadLabyrinth(int n)
        {
            labyrinth = new string[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] inputRow = Console.ReadLine().ToCharArray().Select(x => x + "").ToArray();

                for (int j = 0; j < n; j++)
                {
                    labyrinth[i, j] = inputRow[j];
                }
            }
        }

        private static KeyValuePair<int, int> FindStart(List<KeyValuePair<int, int>> cells)
        {
            return cells.FirstOrDefault(x => labyrinth[x.Key, x.Value] == "*");
        }

        private static List<KeyValuePair<int, int>> GetCells()
        {
            List<KeyValuePair<int, int>> cells = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    cells.Add(new KeyValuePair<int, int>(i, j));
                }
            }

            return cells;
        }

        private static bool IsInMatrix(KeyValuePair<int, int> cell)
        {
            return cell.Key >= 0 && cell.Key < labyrinth.GetLength(0) && cell.Value >= 0 &&
                   cell.Value < labyrinth.GetLength(1);
        }

        private static bool IsVisited(KeyValuePair<int, int> cell)
        {
            return labyrinth[cell.Key, cell.Value] != "0";
        }

        private static int GetValue(KeyValuePair<int, int> cell)
        {
            int result;

            if (int.TryParse(labyrinth[cell.Key, cell.Value], out result))
            {
                return result;
            }

            return 0;
        }

        private static void SetValue(KeyValuePair<int, int> cell, int value)
        {
            labyrinth[cell.Key, cell.Value] = value + "";
        }

        private static int CalculateDistance(KeyValuePair<int, int> rootCell, KeyValuePair<int, int> destinationCell)
        {
            return Math.Abs(rootCell.Key - destinationCell.Key) + Math.Abs(rootCell.Value - destinationCell.Value);
        }

        private static void MarkUnvisited()
        {
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    if (labyrinth[i, j] == "0")
                    {
                        labyrinth[i, j] = "u";
                    }
                }
            }
        }

        private static void PrintLabyrinth()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    result.Append(labyrinth[i, j]);
                }

                result.AppendLine();
            }

            Console.WriteLine(result.ToString());
        }

        private static void WalkIt(KeyValuePair<int, int> startPoint, List<KeyValuePair<int, int>> cells)
        {
            Queue<KeyValuePair<int, int>> traversalQueue = new Queue<KeyValuePair<int, int>>();

            bool hasVisited = true;

            traversalQueue.Enqueue(startPoint);

            while (traversalQueue.Count > 0 || hasVisited)
            {
                hasVisited = false;

                KeyValuePair<int, int> current = traversalQueue.Dequeue();

                List<KeyValuePair<int, int>> checkingCells = new List<KeyValuePair<int, int>>();

                checkingCells.Add(new KeyValuePair<int, int>(current.Key - 1, current.Value));
                checkingCells.Add(new KeyValuePair<int, int>(current.Key, current.Value + 1));
                checkingCells.Add(new KeyValuePair<int, int>(current.Key + 1, current.Value));
                checkingCells.Add(new KeyValuePair<int, int>(current.Key, current.Value - 1));

                foreach (var checkingCell in checkingCells)
                {
                    if (IsInMatrix(checkingCell) && !IsVisited(checkingCell))
                    {
                        hasVisited = true;
                        int distance = CalculateDistance(current, checkingCell) + GetValue(current);
                        SetValue(checkingCell, distance);
                        traversalQueue.Enqueue(checkingCell);
                    }
                }
            }

            MarkUnvisited();
        }
    }
}