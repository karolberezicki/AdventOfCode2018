using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day03
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");

            //source = new[] {"#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2"}; // Sample data

            var claims = source.Select(c => new MaterialClaim(c)).ToList();
            var allPoints = claims.SelectMany(c => c.Points).ToList();

            HashSet<Point> seenPoints = new HashSet<Point>();
            HashSet<Point> overlappingPoints = new HashSet<Point>();

            foreach (var point in allPoints)
            {
                if (seenPoints.Contains(point))
                {
                    overlappingPoints.Add(point);
                }
                else
                {
                    seenPoints.Add(point);
                }
            }

            Console.WriteLine($"Part1: {overlappingPoints.Count}");

            var nonOverlappingClaim = claims.First(claim => !claim.Points.Any(overlappingPoints.Contains));

            Console.WriteLine($"Part1: {nonOverlappingClaim.Id}");

            Console.ReadKey();
        }
    }
}
