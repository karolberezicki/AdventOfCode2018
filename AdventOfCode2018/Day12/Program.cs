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

            List<Pot> pots = initialState.Select((plant, index) => new Pot(index, plant)).ToList();
            for (int i = 1; i < 10; i++)
            {
                pots.Add(new Pot(-1 * i, '.'));
            }

            pots = pots.OrderBy(p => p.Index).ToList();

            for (int i = 1; i <= 20; i++)
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
                updatedPots.Add(new Pot(lastPot.Index+1, '.'));

            }

            var sumOfPots = pots.Where(p => p.Plant == '#').Select(p => p.Index).Sum();

            Console.ReadKey();
        }
    }

    public class Pot
    {
        public Pot(int index, char plant)
        {
            Index = index;
            Plant = plant;
        }

        public int Index { get; set; }
        public char Plant { get; set; }
    }

    public class Note
    {
        public Note(string pattern, char result)
        {
            Pattern = pattern;
            Result = result;
        }

        public string Pattern { get; set; }
        public char Result { get; set; }
    }
}
