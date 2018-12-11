using System;

namespace Day11
{
    public class Program
    {
        private const int GridSerialNumber = 5093;
        private const int GridSize = 300;

        public static void Main(string[] args)
        {
            var grid = new int[GridSize, GridSize];

            GeneratePowerCell(grid);

            var cell = GetCellWithLargestPower(grid, 3);
            Console.WriteLine($"Part1: {cell.X},{cell.Y}");

            for (int size = 3; size < 30; size++)
            {
                var biggerCell = GetCellWithLargestPower(grid, size);

                if (cell.Power < biggerCell.Power)
                {
                    cell = biggerCell;
                }
            }

            Console.WriteLine($"Part2: {cell.X},{cell.Y},{cell.Size}");
            Console.ReadKey();
        }

        private static void GeneratePowerCell(int[,] grid)
        {
            for (int x = 1; x < GridSize; x++)
            {
                for (int y = 1; y < GridSize; y++)
                {
                    var rackId = x + 10;
                    var power = rackId * y;
                    power += GridSerialNumber;
                    power *= rackId;
                    power = power > 100 ? Math.Abs(power / 100) % 10 : 0;
                    power -= 5;

                    grid[x, y] = power;
                }
            }
        }

        private static Cell GetCellWithLargestPower(int[,] grid, int size)
        {
            var cellWithLargestPower = new Cell { X = 0, Y = 0, Power = 0, Size = 0 };
            for (int x = 1; x <= GridSize - size; x++)
            {
                for (int y = 1; y <= GridSize - size; y++)
                {
                    var power = 0;
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            power += grid[x + i, y + j];
                        }
                    }

                    if (cellWithLargestPower.Power < power)
                    {
                        cellWithLargestPower = new Cell { X = x, Y = y, Power = power, Size = size };
                    }
                }
            }

            return cellWithLargestPower;
        }
    }
}
