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
            var elfScores = Enumerable.Range(0, game.Players).Select(p => new List<long>()).ToList();
            var marbleCircle = new LinkedList<int>(new []{0});
            var currentMarble = marbleCircle.First;

            for (int pickedMarble = 1; pickedMarble < game.LastMarbleWorth; pickedMarble++)
            {
                if (pickedMarble % 23 == 0)
                {
                    currentMarble = currentMarble.CircleBackward(marbleCircle, 7);
                    elfScores[(pickedMarble - 1) % game.Players].AddRange(new long[] { pickedMarble, currentMarble.Value });
                    var nextMarble = currentMarble.CircleForward(marbleCircle);
                    marbleCircle.Remove(currentMarble);
                    currentMarble = nextMarble;
                }
                else
                {
                    currentMarble = currentMarble.CircleForward(marbleCircle);
                    var addedMarble = new LinkedListNode<int>(pickedMarble);
                    marbleCircle.AddAfter(currentMarble, addedMarble);
                    currentMarble = addedMarble;
                }
            }

            return elfScores.Select(s => s.Sum()).Max();
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
