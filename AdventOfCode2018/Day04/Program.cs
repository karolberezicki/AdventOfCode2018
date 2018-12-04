using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");

            var guards = CalcGuardsSleep(source);

            var selectedGuardByStrategyOne = guards.OrderByDescending(g => g.MinutesAsleep.Count).First();

            int mostFrequentMinute = GetMostFrequentSleepMinute(selectedGuardByStrategyOne).Minute;

            var part1 = selectedGuardByStrategyOne.Id * mostFrequentMinute;
            Console.WriteLine($"Part1: {part1}");

            var selectedGuardByStrategyTwo = guards.Where(g => g.MinutesAsleep.Count > 0)
                .Select(g => new { g.Id, MinuteCount = GetMostFrequentSleepMinute(g) })
                .OrderByDescending(g => g.MinuteCount.Count)
                .First();

            var part2 = selectedGuardByStrategyTwo.Id * selectedGuardByStrategyTwo.MinuteCount.Minute;

            Console.WriteLine($"Part2: {part2}");

            Console.ReadKey();
        }

        private static HashSet<Guard> CalcGuardsSleep(IEnumerable<string> log)
        {
            var guards = new HashSet<Guard>();
            var chronologicalLog = log.OrderBy(c => c).ToList();
            int currentGuardId = 0;
            int currentSleepStart = 0;
            Regex minutesRegex = new Regex(@"^\S{11}\s\S{3}(?<Minutes>\d{2})");

            foreach (var entry in chronologicalLog)
            {
                if (entry.Contains("Guard"))
                {
                    currentGuardId = int.Parse(entry.Split("#")[1].Split(" begins")[0]);
                    if (guards.All(g => g.Id != currentGuardId))
                    {
                        guards.Add(new Guard(currentGuardId));
                    }
                    continue;
                }

                if (entry.Contains("falls asleep"))
                {
                    currentSleepStart = int.Parse(minutesRegex.Match(entry).Groups["Minutes"].Value);
                    continue;
                }

                if (entry.Contains("wakes up"))
                {
                    int currentSleepEnd = int.Parse(minutesRegex.Match(entry).Groups["Minutes"].Value);
                    var currentSleepMinutes = Enumerable.Range(currentSleepStart, currentSleepEnd - currentSleepStart);
                    guards.First(g => g.Id == currentGuardId).MinutesAsleep.AddRange(currentSleepMinutes);
                }
            }

            return guards;
        }

        private static (int Minute, int Count) GetMostFrequentSleepMinute(Guard guard)
        {
            var i = guard.MinutesAsleep
                .GroupBy(s => s)
                .Select(g => new {Item = g.Key, Count = g.Count()})
                .OrderByDescending(g => g.Count)
                .First();

            return (i.Item, i.Count);
        }
    }
}
