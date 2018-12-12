using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");

            var initialState = source.First().Replace("initial state: ", "").Trim().ToList();
            var notes = source.Skip(2).Select(c => c.Split(" => ")).Select(n => new Note(n[0], n[1].First())).ToList();

            var part1 = SumAfterTwentyGenerations(initialState, notes);

            Console.WriteLine($"Part1: {part1}");

            var part2 = SumAfterFiftyBillionGenerations(initialState, notes);

            Console.WriteLine($"Part2: {part2}");

            Console.ReadKey();
        }

        private static long SumAfterFiftyBillionGenerations(List<char> initialState, List<Note> notes)
        {
            List<Pot> pots = InitPots(initialState);

            List<(int Iteration, int Result)> results = new List<(int Iteration, int Result)>();

            int iteration = 0;
            while (results.Count < 2)
            {
                iteration++;

                pots = UpdatePots(notes, pots);

                if (iteration % 1000 != 0)
                {
                    continue;
                }

                var sumOfPots = pots.Where(p => p.Plant == '#').Select(p => p.Index).Sum();
                results.Add((iteration, sumOfPots));
            }

            long generations = 50000000000;

            var sumOfPotsAfterFiftyBillionGenerations = (generations - 1000) / 1000 * (results[1].Result - results[0].Result) + results[0].Result;
            return sumOfPotsAfterFiftyBillionGenerations;
        }

        private static int SumAfterTwentyGenerations(List<char> initialState, List<Note> notes)
        {
            List<Pot> pots = InitPots(initialState);

            for (int i = 1; i <= 20; i++)
            {
                pots = UpdatePots(notes, pots);
            }
            return pots.Where(p => p.Plant == '#').Select(p => p.Index).Sum();
        }

        private static List<Pot> UpdatePots(IReadOnlyCollection<Note> notes, List<Pot> pots)
        {
            List<Pot> updatedPots = new List<Pot>();

            for (int index = 0; index < pots.Count; index++)
            {
                int leftEdgeIndex = index - 2;
                int leftIndex = index - 1;
                int rightIndex = index + 1;
                int rightEdgeIndex = index + 2;

                var leftEdge = pots.ElementAtOrDefault(leftEdgeIndex)?.Plant ?? '.';
                var left = pots.ElementAtOrDefault(leftIndex)?.Plant ?? '.';
                var right = pots.ElementAtOrDefault(rightIndex)?.Plant ?? '.';
                var rightEdge = pots.ElementAtOrDefault(rightEdgeIndex)?.Plant ?? '.';

                var pot = pots[index];

                var pattern = $"{leftEdge}{left}{pot.Plant}{right}{rightEdge}";

                var updatedPot = new Pot(pot.Index, notes.FirstOrDefault(n => n.Pattern == pattern)?.Result ?? '.');
                updatedPots.Add(updatedPot);
            }

            pots = updatedPots;
            var lastPot = pots.Last();
            updatedPots.Add(new Pot(lastPot.Index + 1, '.'));
            return pots;
        }

        private static List<Pot> InitPots(IEnumerable<char> initialState)
        {
            List<Pot> pots = initialState.Select((plant, index) => new Pot(index, plant)).ToList();
            for (int i = 1; i < 10; i++)
            {
                pots.Add(new Pot(-1 * i, '.'));
            }

            pots = pots.OrderBy(p => p.Index).ToList();
            return pots;
        }
    }
}
