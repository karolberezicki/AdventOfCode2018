using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");

            var coordinates = source
                .Select(coordinate => coordinate.Split(", "))
                .Select(coordinate => (X: int.Parse(coordinate[0]), Y: int.Parse(coordinate[1])))
                .ToList();

            int planeSize = coordinates.Select(p => p.X).Union(coordinates.Select(p => p.X)).Max() + 1;

            var matrix = new int[planeSize, planeSize];
            var closestDistanceMatrix = new int[planeSize, planeSize];
            closestDistanceMatrix.FillArray(int.MaxValue);

            for (int index = 0; index < coordinates.Count; index++)
            {
                var point = coordinates[index];
                matrix[point.X, point.Y] = index;
                closestDistanceMatrix[point.X, point.Y] = 0;
            }

            for (int x = 0; x < planeSize; x++)
            {
                for (int y = 0; y < planeSize; y++)
                {
                    for (int id = 0; id < coordinates.Count; id++)
                    {
                        var point = coordinates[id];

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

            List<int> area = matrix.ToList();
            var borderPointsIndexes = matrix.GetRow(0)
                .Union(matrix.GetRow(planeSize - 1))
                .Union(matrix.GetCol(0))
                .Union(matrix.GetCol(planeSize - 1));

            var part1 = area
                .Where(p => !borderPointsIndexes.Contains(p) && p != -1)
                .GroupBy(d => d)
                .Select(d => (Id: d.Key, Count: d.Count()))
                .OrderByDescending(d => d.Count)
                .ToList().First().Count;

            Console.WriteLine($"Part1: {part1}");

            var region = new int[planeSize, planeSize];
            for (int x = 0; x < planeSize; x++)
            {
                for (int y = 0; y < planeSize; y++)
                {
                    foreach (var point in coordinates)
                    {
                        var distance = Math.Abs(point.X - x) + Math.Abs(point.Y - y);

                        if (distance >= 10000)
                        {
                            region[x, y] = int.MaxValue;
                            break;
                        }

                        region[x, y] += distance;
                    }
                }
            }

            var part2 = region.ToList().Count(a => a < 10000);

            Console.WriteLine($"Part1: {part2}");

            Console.ReadKey();
        }
    }
}
