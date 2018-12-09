using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day09
{
    public class Program
    {
        public static void Main()
        {
            long part1 = CalculateHighScore(GetGameData());
            Console.WriteLine($"Part1: {part1}");

            long part2 = CalculateHighScore(GetGameData(100));
            Console.WriteLine($"Part1: {part2}");

            Console.ReadKey();
        }

        private static long CalculateHighScore((int Players, int LastMarbleWorth) game)
        {
            List<List<long>> elfScores = Enumerable.Range(0, game.Players).Select(p => new List<long>()).ToList();

            LinkedList<int> marbleCircle = new LinkedList<int>();
            marbleCircle.AddFirst(new LinkedListNode<int>(0));
            int pickedMarble = 0;
            int currentElf = 0;

            LinkedListNode<int> currentMarble = marbleCircle.First;

            while (pickedMarble <= game.LastMarbleWorth)
            {
                pickedMarble++;
                if (pickedMarble % 23 == 0)
                {
                    currentMarble = StepBack(marbleCircle, currentMarble, 7);
                    elfScores[currentElf].AddRange(new long[] { pickedMarble, currentMarble.Value });

                    var nextMarble = currentMarble.Next ?? marbleCircle.First;
                    marbleCircle.Remove(currentMarble);
                    currentMarble = nextMarble;
                }
                else
                {
                    currentMarble = currentMarble.Next ?? marbleCircle.First;
                    var addedMarble = new LinkedListNode<int>(pickedMarble);
                    marbleCircle.AddAfter(currentMarble, addedMarble);
                    currentMarble = addedMarble;
                }

                currentElf = ++currentElf % game.Players;
            }

            return elfScores.Select(s => s.Sum()).Max();
        }

        private static LinkedListNode<int> StepBack(LinkedList<int> list, LinkedListNode<int> node, int times)
        {
            for (int i = 0; i < times; i++)
            {
                node = node.Previous ?? list.Last;
            }

            return node;
        }

        private static (int Players, int LastMarbleWorth) GetGameData(int scoreMultiplier = 1)
        {
            var source = File.ReadAllText(@"..\..\..\input.txt");
            var descriptionRegex = new Regex(@"(?<Players>\d+) players; last marble is worth (?<Points>\d+) points");
            Match match = descriptionRegex.Match(source);
            return (int.Parse(match.Groups["Players"].Value), int.Parse(match.Groups["Points"].Value)*scoreMultiplier);
        }
    }
}
