using System;
using System.IO;
using System.Linq;

namespace Day08
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllText(@"..\..\..\input.txt");

            var license = source.Split(" ").Select(int.Parse).ToList();

            var root = Node.CreateNode(license, 0);
            var sumMetadata = root.Node.SumMetadata();
            Console.WriteLine($"Part1: {sumMetadata}");

            var value = root.Node.CalcValue();
            Console.WriteLine($"Part1: {value}");

            Console.ReadKey();
        }
    }
}
