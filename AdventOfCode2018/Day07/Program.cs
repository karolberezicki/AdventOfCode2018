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

            List<char> completed = new List<char> {notBlocked.First()};

            while (completed.Count < allStepsCount)
            {
                var candidates = notBlocked.Union(steps.Where(s => completed.Contains(s.Before)).Select(s => s.After))
                    .Except(completed)
                    .OrderBy(c => c);

                foreach (var candidate in candidates)
                {
                    if (steps.Where(s => s.After == candidate).Select(s => s.Before).Except(completed).Any())
                    {
                        continue;
                    }

                    completed.Add(candidate);
                    break;
                }
            }

            var part1 = string.Concat(completed);

            Console.ReadKey();
        }
    }
}
