using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    public enum Turn
    {
        Left,
        Straight,
        Right
    }

    public class Program
    {
        private const char Up = '^';
        private const char Down = 'v';
        private const char Left = '<';
        private const char Right = '>';

        private static readonly Dictionary<(char symbol, char track), char> CurveLogic = new Dictionary<(char symbol, char track), char>
        {
            {(Left,'/'),Down},
            {(Up,'/'),Right},
            {(Right,'/'),Up},
            {(Down,'/'),Left},
            {(Left,'\\'),Up},
            {(Up,'\\'),Left},
            {(Right,'\\'),Down},
            {(Down,'\\'),Right},
        };

        private static readonly Dictionary<(char symbol, Turn turn), char> TurnLogic = new Dictionary<(char symbol, Turn turn), char>
        {
            {(Right,Turn.Left),Up},
            {(Right,Turn.Straight),Right},
            {(Right,Turn.Right),Down},

            {(Left,Turn.Left),Down},
            {(Left,Turn.Straight),Left},
            {(Left,Turn.Right),Up},

            {(Up,Turn.Left),Left},
            {(Up,Turn.Straight),Up},
            {(Up,Turn.Right),Right},

            {(Down,Turn.Left),Right},
            {(Down,Turn.Straight),Down},
            {(Down,Turn.Right),Left}
        };

        private static readonly Dictionary<Turn, Turn> IntersectionLogic = new Dictionary<Turn, Turn>
        {
            {Turn.Left,Turn.Straight},
            {Turn.Straight,Turn.Right},
            {Turn.Right,Turn.Left},
        };

        // TODO: Refactor cart to class :)
        public static void Main()
        {
            var source = File.ReadAllLines(@"..\..\..\input.txt");

            var height = source.Length;
            var width = source[0].Length;

            var grid = new char[width, height];

            var cartSymbols = new[] { Up, Down, Left, Right };

            var carts = new List<(int X, int Y, char Symbol, Turn NextTurn, bool Crashed)>();


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var track = source[y][x];

                    if (cartSymbols.Contains(track))
                    {
                        grid[x, y] = track == Up || track == Down ? '|' : '-';
                        carts.Add((x, y, track, Turn.Left, false));
                    }
                    else
                    {
                        grid[x, y] = track;
                    }
                }
            }

            string part1 = null;
            string part2 = null;

            while (true)
            {
                if (!string.IsNullOrWhiteSpace(part2))
                {
                    break;
                }

                carts = carts.OrderBy(c => c.Y).ThenBy(c => c.X).ToList();

                for (int index = 0; index < carts.Count; index++)
                {
                    if (carts[index].Crashed)
                    {
                        continue;
                    }

                    var cart = CartMove(carts[index], grid);

                    if (carts.Any(c => c.X == cart.X && c.Y == cart.Y && c.Crashed == cart.Crashed))
                    {

                        if (string.IsNullOrWhiteSpace(part1))
                        {
                            part1 = $"{cart.X},{cart.Y}";
                            Console.WriteLine($"Part1: {part1}");
                        }

                        cart = MarkAsCrashed(cart);
                    }

                    carts[index] = cart;

                    if (cart.Crashed)
                    {
                        for (int i = 0; i < carts.Count; i++)
                        {
                            if (carts[i].X == cart.X && carts[i].Y == cart.Y)
                            {
                                carts[i] = MarkAsCrashed(carts[i]);
                            }
                        }
                    }

                    if (carts.Count(c => c.Crashed == false) == 1)
                    {
                        var lastCart = carts.First(c => c.Crashed == false);
                        part2 = $"{lastCart.X},{lastCart.Y}";
                        Console.WriteLine($"Part2: {part2}");
                    }
                }
            }

            Console.ReadKey();
        }

        public static (int X, int Y, char Symbol, Turn NextTurn, bool Crashed) MarkAsCrashed((int X, int Y, char Symbol, Turn NextTurn, bool Crashed) cart)
        {
            return (cart.X, cart.Y, cart.Symbol, cart.NextTurn, true);
        }

        public static (int X, int Y, char Symbol, Turn NextTurn, bool Crashed) CartMove((int X, int Y, char Symbol, Turn NextTurn, bool Crashed) cart, char[,] grid)
        {
            int x;
            int y;

            switch (cart.Symbol)
            {
                case Up:
                    x = cart.X;
                    y = cart.Y - 1;
                    break;
                case Down:
                    x = cart.X;
                    y = cart.Y + 1;
                    break;
                case Left:
                    x = cart.X - 1;
                    y = cart.Y;
                    break;
                case Right:
                    x = cart.X + 1;
                    y = cart.Y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var track = grid[x, y];

            if (track == '+')
            {
                return (x, y, TurnLogic[(cart.Symbol, cart.NextTurn)], IntersectionLogic[cart.NextTurn], cart.Crashed);
            }

            var key = (cart.Symbol, track);
            return (x, y, CurveLogic.ContainsKey(key) ? CurveLogic[key] : cart.Symbol, cart.NextTurn, cart.Crashed);
        }
    }
}
