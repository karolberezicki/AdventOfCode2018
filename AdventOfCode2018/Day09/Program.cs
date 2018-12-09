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
            var game = GetGameData();

            int highScore = CalculateHighScore(game);
            Console.WriteLine(highScore);

            int highScorePart2 = CalculateHighScore((game.Players, game.LastMarbleWorth*100));
            Console.WriteLine(highScorePart2);

            Console.ReadKey();
        }

        private static int CalculateHighScore((int Players, int LastMarbleWorth) game)
        {
            List<List<int>> elfScores = new List<List<int>>();
            for (int i = 0; i < game.Players; i++)
            {
                elfScores.Add(new List<int>());
            }

            List<int> marbleCircle = new List<int> { 0 };
            int pickedMarble = 1;

            int currentElf = 0;
            int currentMarbleIndex = 0;

            while (pickedMarble <= game.LastMarbleWorth)
            {

                if (pickedMarble % 23 == 0)
                {
                    currentMarbleIndex = currentMarbleIndex - 7;
                    currentMarbleIndex = currentMarbleIndex >= 0
                        ? currentMarbleIndex
                        : marbleCircle.Count + currentMarbleIndex;
                    var value = marbleCircle[currentMarbleIndex];
                    marbleCircle.RemoveAt(currentMarbleIndex);
                    elfScores[currentElf].Add(pickedMarble);
                    elfScores[currentElf].Add(value);
                }
                else
                {
                    currentMarbleIndex += 1;
                    currentMarbleIndex = currentMarbleIndex % marbleCircle.Count;
                    currentMarbleIndex++;
                    marbleCircle.Insert(currentMarbleIndex, pickedMarble);

                }

                pickedMarble++;
                currentElf++;
                currentElf = currentElf % game.Players;

                if (pickedMarble % 10000 == 0)
                {
                    Console.WriteLine($"Current picked marble {pickedMarble}");
                }
            }

            var highScore = elfScores.Select(s => s.Sum()).Max();
            return highScore;
        }

        private static (int Players, int LastMarbleWorth) GetGameData()
        {
            var source = File.ReadAllText(@"..\..\..\input.txt");
            var descriptionRegex = new Regex(@"(?<Players>\d+) players; last marble is worth (?<Points>\d+) points");
            Match match = descriptionRegex.Match(source);
            return (int.Parse(match.Groups["Players"].Value), int.Parse(match.Groups["Points"].Value));
        }
    }
}
