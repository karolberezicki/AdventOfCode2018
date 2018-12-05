using System;
using System.IO;
using System.Text;

namespace Day05
{
    public class Program
    {
        public static void Main()
        {
            var sourcePolymer = File.ReadAllText(@"..\..\..\input.txt").Trim();

            var sourcePolymerAfterReaction = PerformReaction(sourcePolymer);

            Console.WriteLine($"Part1: {sourcePolymerAfterReaction.Length}");

            int shortestPolymerLength = int.MaxValue;
            for (int i = 65; i <= 90; i++)
            {
                var polymer = sourcePolymer.Replace(((char)i).ToString(), "").Replace(((char)(i + 32)).ToString(), "");
                var polymerAfterReaction = PerformReaction(polymer);

                if (polymerAfterReaction.Length < shortestPolymerLength)
                {
                    shortestPolymerLength = polymerAfterReaction.Length;
                }
            }

            Console.WriteLine($"Part2: {shortestPolymerLength}");

            Console.ReadKey();
        }

        private static string PerformReaction(string polymer)
        {
            bool reacted = true;
            while (reacted)
            {
                reacted = false;
                for (int index = 0; index < polymer.Length - 1; index++)
                {
                    char unit = polymer[index];
                    char nextUnit = polymer[index + 1];

                    if (Math.Abs(unit - nextUnit) - 32 != 0)
                    {
                        continue;
                    }

                    reacted = true;
                    polymer = new StringBuilder(polymer).Remove(index, 2).ToString();

                    break;
                }
            }

            return polymer;
        }
    }
}
