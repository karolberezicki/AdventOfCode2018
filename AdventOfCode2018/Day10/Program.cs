using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");
            var points = source.Select(s => new Point(s, 0)).ToList();
            var requiredDeltaX = points.Where(p => p.DeltaX != 0).Select(p => p.X / p.DeltaX).Max();
            var requiredDeltaY = points.Where(p => p.DeltaY != 0).Select(p => p.Y / p.DeltaY).Max();
            var initTime = Math.Max(Math.Abs(requiredDeltaX), Math.Abs(requiredDeltaY));
            points = source.Select(s => new Point(s, initTime)).ToList();

            int passedSeconds = 0;

            while (!DetectEdge(points))
            {
                passedSeconds++;
                foreach (Point point in points)
                {
                    point.Move();
                }
            }

            Normalize(points);
            Print(points);

            Console.WriteLine($"Required time: {passedSeconds + initTime}");

            Console.ReadKey();
        }

        private static void Print(IReadOnlyCollection<Point> points)
        {
            Console.WriteLine();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 80; x++)
                {
                    Console.Write(points.Any(p => p.InPosition(x, y)) ? "█" : " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void Normalize(IReadOnlyCollection<Point> points)
        {
            var minX = points.Select(p => p.X).Min();
            var minY = points.Select(p => p.Y).Min();

            foreach (Point point in points)
            {
                point.X -= minX;
                point.Y -= minY;
            }
        }

        private static bool DetectEdge(IReadOnlyCollection<Point> points)
        {
            return points.Any(point => new[]
            {
                points.Any(p => p.X == point.X && p.Y == point.Y - 1),
                points.Any(p => p.X == point.X && p.Y == point.Y - 2),
                points.Any(p => p.X == point.X && p.Y == point.Y - 3),
                points.Any(p => p.X == point.X + 1 && p.Y == point.Y),
                points.Any(p => p.X == point.X + 2 && p.Y == point.Y),
                points.Any(p => p.X == point.X + 3 && p.Y == point.Y)
            }.All(c => c));
        }
    }
}
