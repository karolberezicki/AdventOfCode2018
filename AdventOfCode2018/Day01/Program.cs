using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");
            List<int> deltas = source.Select(int.Parse).ToList();

            var part1 = deltas.Sum();
            Console.WriteLine($"Part1: {part1}");

            var part2 = FindFirstRepeatedFrequency(deltas);
            Console.WriteLine($"Part1: {part2}");

            Console.ReadKey();
        }

        private static int FindFirstRepeatedFrequency(IReadOnlyCollection<int> deltas)
        {
            int currentFrequency = 0;
            HashSet<int> seenFrequencies = new HashSet<int> { currentFrequency };
            while (true)
            {
                foreach (int delta in deltas)
                {
                    currentFrequency += delta;
                    if (seenFrequencies.Contains(currentFrequency))
                    {
                        return currentFrequency;
                    }

                    seenFrequencies.Add(currentFrequency);
                }
            }
        }
    }
}
