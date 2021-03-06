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

            var sourcePolymerAfterReaction = PerformReaction(new StringBuilder(sourcePolymer));

            Console.WriteLine($"Part1: {sourcePolymerAfterReaction.Length}");

            int shortestPolymerLength = int.MaxValue;
            for (int i = 65; i <= 90; i++)
            {
                var polymer = new StringBuilder(sourcePolymerAfterReaction);
                polymer.Replace(((char)i).ToString(), "");
                polymer.Replace(((char)(i + 32)).ToString(), "");

                var polymerAfterReaction = PerformReaction(polymer);

                if (polymerAfterReaction.Length < shortestPolymerLength)
                {
                    shortestPolymerLength = polymerAfterReaction.Length;
                }
            }

            Console.WriteLine($"Part2: {shortestPolymerLength}");

            Console.ReadKey();
        }

        private static string PerformReaction(StringBuilder polymer)
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
                    polymer.Remove(index, 2);

                    break;
                }
            }

            return polymer.ToString();
        }
    }
}
