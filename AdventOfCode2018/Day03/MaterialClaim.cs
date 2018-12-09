using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day03
{
    [DebuggerDisplay("{Display}")]
    public class MaterialClaim
    {
        private readonly Regex _descriptionRegex = new Regex(@"#(?<Id>\d+) @ (?<Left>\d+),(?<Top>\d+): (?<Width>\d+)x(?<Height>\d+)");

        public int Id { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public string Display => $"#{Id} @ {Left},{Top}: {Width}x{Height}";

        public HashSet<(int X, int Y)> Points { get; set; }

        public MaterialClaim(string description)
        {
            Match match = _descriptionRegex.Match(description);
            Id = int.Parse(match.Groups[nameof(Id)].Value);
            Left = int.Parse(match.Groups[nameof(Left)].Value);
            Top = int.Parse(match.Groups[nameof(Top)].Value);
            Width = int.Parse(match.Groups[nameof(Width)].Value);
            Height = int.Parse(match.Groups[nameof(Height)].Value);
            Points = new HashSet<(int X, int Y)>();
            for (int x = Left; x < Left + Width; x++)
            {
                for (int y = Top; y < Top + Height; y++)
                {
                    Points.Add((x, y));
                }
            }
        }
    }
}
