using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");
            var descriptionRegex = new Regex(@"Step (?<Before>\w+) must be finished before step (?<After>\w+) can begin.");

            var steps = source.Select(i =>
            {
                Match match = descriptionRegex.Match(i);
                return (Before: match.Groups["Before"].Value.First(), After: match.Groups["After"].Value.First());
            }).ToList();

            var allStepsCount = steps.Select(s => s.Before).Union(steps.Select(s => s.After)).Count();

            var notBlocked = steps.Select(s => s.Before).Except(steps.Select(s => s.After))
                .OrderBy(c => c)
                .ToList();

            List<char> stepsOrder = new List<char> {notBlocked.First()};

            while (stepsOrder.Count < allStepsCount)
            {
                var candidates = notBlocked.Union(steps.Where(s => stepsOrder.Contains(s.Before)).Select(s => s.After))
                    .Except(stepsOrder)
                    .OrderBy(c => c);

                foreach (var candidate in candidates)
                {
                    if (steps.Where(s => s.After == candidate).Select(s => s.Before).Except(stepsOrder).Any())
                    {
                        continue;
                    }

                    stepsOrder.Add(candidate);
                    break;
                }
            }

            Console.WriteLine($"Part1: {string.Concat(stepsOrder)}");

            List<char> multiElfCompleted = new List<char>();
            const int workersCount = 5;
            int time = 0;
            var assignedWork = new List<(char Letter, int TimeLeft)>();
            while (multiElfCompleted.Count < stepsOrder.Count)
            {
                var blocked = steps.Where(s => !multiElfCompleted.Contains(s.Before))
                    .Select(s => s.After);

                var assignableLetters = stepsOrder
                    .Except(multiElfCompleted)
                    .Except(assignedWork.Select(w => w.Letter))
                    .Except(blocked)
                    .OrderBy(c => c);

                foreach (char assignableLetter in assignableLetters)
                {
                    if (assignedWork.Count >= workersCount)
                    {
                        break;
                    }
                    assignedWork.Add((assignableLetter, assignableLetter - 4));
                }

                var assignedWorkAfterIteration = new List<(char Letter, int TimeLeft)>();
                foreach (var work in assignedWork)
                {
                    if (work.TimeLeft == 1)
                    {
                        multiElfCompleted.Add(work.Letter);
                    }
                    else
                    {
                        assignedWorkAfterIteration.Add((work.Letter, work.TimeLeft-1));
                    }
                }

                assignedWork = assignedWorkAfterIteration;

                time++;
            }

            Console.WriteLine($"Part2: {time}");

            Console.ReadKey();
        }
    }
}
