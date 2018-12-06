using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day06
{
    public class Program
    {
        private const int PlaneSize = 400;

        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");

            var coordinates = source
                .Select(coordinate => coordinate.Split(", "))
                .Select(coordinate => new Point(int.Parse(coordinate[0]), int.Parse(coordinate[1])))
                .ToList();

            var matrix = new int[PlaneSize, PlaneSize];
            var closestDistanceMatrix = new int[PlaneSize, PlaneSize];
            closestDistanceMatrix.FillArray(int.MaxValue);

            for (int index = 0; index < coordinates.Count; index++)
            {
                Point point = coordinates[index];
                matrix[point.X, point.Y] = index;
                closestDistanceMatrix[point.X, point.Y] = 0;
            }

            for (int x = 0; x < PlaneSize; x++)
            {
                for (int y = 0; y < PlaneSize; y++)
                {
                    for (int id = 0; id < coordinates.Count; id++)
                    {
                        Point point = coordinates[id];

                        if (closestDistanceMatrix[x, y] == 0)
                        {
                            break;
                        }

                        var distance = Math.Abs(point.X - x) + Math.Abs(point.Y - y);

                        if (closestDistanceMatrix[x, y] != 0 && closestDistanceMatrix[x, y] == distance)
                        {
                            matrix[x, y] = -1;
                        }

                        if (closestDistanceMatrix[x, y] > distance)
                        {
                            closestDistanceMatrix[x, y] = distance;
                            matrix[x, y] = id;
                        }

                    }
                }
            }

            Display(coordinates, matrix);

            List<int> area = matrix.ToList();
            var borderPointsIndexes = matrix.GetRow(0)
                .Union(matrix.GetRow(PlaneSize - 1))
                .Union(matrix.GetCol(0))
                .Union(matrix.GetCol(PlaneSize - 1));

            var part1 = area
                .Where(p => !borderPointsIndexes.Contains(p) && p != -1)
                .GroupBy(d => d)
                .Select(d => (Id: d.Key, Count: d.Count()))
                .OrderByDescending(d => d.Count)
                .ToList().First();


            Console.ReadKey();
        }

        private static void Display(List<Point> points, int[,] matrix)
        {
            if (matrix.Length > 50)
            {
                return;
            }

            for (int x = 0; x < PlaneSize; x++)
            {
                for (int y = 0; y < PlaneSize; y++)
                {
                    if (matrix[y, x] == -1)
                    {
                        Console.Write(".");
                        continue;
                    }

                    if (points.Count(p => p.X == x && p.Y == y) == 0)
                    {
                        Console.Write((char)(matrix[y, x] + 97));
                    }
                    else
                    {
                        Console.Write((char)(matrix[y, x] + 65));
                    }
                }

                Console.WriteLine();
            }
        }


        public static void FillArray(int[,] array, int value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = value;
                }
            }
        }

    }

    public static class MatrixExtensions
    {
        public static T[] GetRow<T>(this T[,] matrix, int row)
        {
            var rowLength = matrix.GetLength(1);
            var rowVector = new T[rowLength];

            for (var i = 0; i < rowLength; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }

        public static T[] GetCol<T>(this T[,] matrix, int col)
        {
            var colLength = matrix.GetLength(0);
            var colVector = new T[colLength];

            for (var i = 0; i < colLength; i++)
                colVector[i] = matrix[i, col];

            return colVector;
        }

        public static List<T> ToList<T>(this T[,] matrix)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            List<T> result = new List<T>(width * height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    result.Add(matrix[i, j]);
                }
            }
            return result;
        }

        public static void FillArray<T>(this T[,] array, T value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = value;
                }
            }
        }
    }
}
