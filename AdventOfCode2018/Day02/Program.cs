using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    public class Program
    {
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");

            var frequencies = source.Select(CountLetterFrequencies).ToList();

            var countIdsWithTwoLetters = frequencies.Count(f => f.Values.Contains(2));
            var countIdsWithThreeLetters = frequencies.Count(f => f.Values.Contains(3));

            var part1 = countIdsWithTwoLetters * countIdsWithThreeLetters;
            Console.WriteLine($"Part1: {part1}");

            var part2 = FindCommonLetters(source);
            Console.WriteLine($"Part1: {part2}");

            Console.ReadKey();
        }

        private static string FindCommonLetters(string[] ids)
        {
            foreach (string id in ids)
            {
                foreach (string comparingId in ids)
                {
                    List<char> commonLetters = new List<char>();
                    for (int i = 0; i < comparingId.Length; i++)
                    {
                        if (comparingId[i] == id[i])
                        {
                            commonLetters.Add(id[i]);
                        }
                    }

                    if (commonLetters.Count == id.Length - 1)
                    {
                        return string.Concat(commonLetters); // most efficient way for .Net Core 2.1
                    }
                }
            }
            throw new Exception("Not found.");
        }

        private static Dictionary<char, int> CountLetterFrequencies(string id)
        {
            Dictionary<char, int> frequencies = new Dictionary<char, int>();

            foreach (char letter in id)
            {
                frequencies[letter] = frequencies.ContainsKey(letter) ? frequencies[letter] + 1 : 1;
            }

            return frequencies;
        }

    }
}
