using Advent_of_Code_2024.Day8.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.Shared.Enum;
using AdventofCode.Core.Shared.Models;
using AdventofCode.Core.Shared.Services;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Advent_of_Code_2024.Day8
{
    public class Day8 : BasePuzzle
    {
        public List<string> Data { get; set; }

        private List<StringValueCoordinate> AntennaLocations = new List<StringValueCoordinate>();
        private List<Coordinate> AntiNodeLocations = new List<Coordinate>();

        private int MaxX;
        private int MaxY;

        public Day8(PuzzleTools tools) : base(tools)
        {
            Data = new List<string>();
        }

        public async void Challenge1()
        {

            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            Data = inputData.Lines;

            MaxX = Data[0].Length - 1;
            MaxY = Data.Count - 1;

            AddAntennaCoordinates();

            for (var i = 0; i < AntennaLocations.Count - 1; i++)
            {
                for (var j = i + 1; j < AntennaLocations.Count; j++)
                {
                    var firstLocation = AntennaLocations[i];
                    var secondLocation = AntennaLocations[j];

                    if (!firstLocation.Value.Equals(secondLocation.Value))
                    {
                        continue;
                    }

                    var xDifference = firstLocation.XCoordinate - secondLocation.XCoordinate;
                    var yDifference = firstLocation.YCoordinate - secondLocation.YCoordinate;

                    if (firstLocation.XCoordinate + xDifference >= 0
                        && firstLocation.XCoordinate + xDifference <= MaxX
                        && firstLocation.YCoordinate + yDifference >= 0
                        && firstLocation.YCoordinate + yDifference <= MaxY)
                    {
                        var antiNodeCoordinate = new Coordinate()
                        {
                            XCoordinate = firstLocation.XCoordinate + xDifference,
                            YCoordinate = firstLocation.YCoordinate + yDifference
                        };

                        var existingAntinode = AntiNodeLocations.FirstOrDefault(x => x.XCoordinate == antiNodeCoordinate.XCoordinate && x.YCoordinate == antiNodeCoordinate.YCoordinate);

                        if (existingAntinode == null)
                        {
                            AntiNodeLocations.Add(antiNodeCoordinate);
                        }
                    }

                    if (secondLocation.XCoordinate - xDifference >= 0
                       && secondLocation.XCoordinate - xDifference <= MaxX
                       && secondLocation.YCoordinate - yDifference >= 0
                       && secondLocation.YCoordinate - yDifference <= MaxY)
                    {
                        var antiNodeCoordinate = new Coordinate()
                        {
                            XCoordinate = secondLocation.XCoordinate - xDifference,
                            YCoordinate = secondLocation.YCoordinate - yDifference
                        };

                        var existingAntinode = AntiNodeLocations.FirstOrDefault(x => x.XCoordinate == antiNodeCoordinate.XCoordinate && x.YCoordinate == antiNodeCoordinate.YCoordinate);

                        if (existingAntinode == null)
                        {
                            AntiNodeLocations.Add(antiNodeCoordinate);
                        }
                    }
                }

            }


            Console.WriteLine($"Solution is {AntiNodeLocations.Count}");
            return;
        }

        public async void Challenge2()
        {
            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            Data = inputData.Lines;

            MaxX = Data[0].Length - 1;
            MaxY = Data.Count - 1;

            AddAntennaCoordinates();

            for (var i = 0; i < AntennaLocations.Count - 1; i++)
            {
                for (var j = i + 1; j < AntennaLocations.Count; j++)
                {
                    var firstLocation = AntennaLocations[i];
                    var secondLocation = AntennaLocations[j];

                    if (!firstLocation.Value.Equals(secondLocation.Value))
                    {
                        continue;
                    }

                     GetLinePoints(firstLocation, secondLocation);

                }
            }


            Console.WriteLine($"Solution is {AntiNodeLocations.Count}");
            return;
        }

        private async Task<Day8Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day8Model>(8, false).ConfigureAwait(false);
        }

        private void AddAntennaCoordinates()
        {
            for (var i = 0; i < Data.Count; i++)
            {
                var line = Data[i];
                for (var j = 0; j < line.Length; j++)
                {
                    if (line[j] != '.')
                    {
                        AntennaLocations.Add(new StringValueCoordinate()
                        {
                            Value = line[j].ToString(),
                            XCoordinate = j,
                            YCoordinate = i,
                        });

                    }
                }
            }
        }

        private void GetLinePoints(Coordinate firstPoint, Coordinate secondPoint)
        {
            var points = new List<Coordinate>();

            var dx = secondPoint.XCoordinate - firstPoint.XCoordinate;
            var dy = secondPoint.YCoordinate - firstPoint.YCoordinate;

            //normalise slope into simplest terms
            int gcd = GCD(Math.Abs(dx), Math.Abs(dy));

            dx /= gcd;
            dy /= gcd;

            int currentX = firstPoint.XCoordinate;

            int currentY = firstPoint.YCoordinate;

            //positive direction
            while (currentX >= 0 && currentY >= 0 && currentX <= MaxX && currentY <= MaxY)
            {
                var existingPoint = AntiNodeLocations.FirstOrDefault(p => p.XCoordinate == currentX && p.YCoordinate == currentY);

                if (existingPoint == null)
                {
                    AntiNodeLocations.Add(new Coordinate() { XCoordinate = currentX, YCoordinate = currentY });

                }
                currentX = currentX + dx;
                currentY = currentY + dy;
            }

            currentX = firstPoint.XCoordinate;
            currentY = firstPoint.YCoordinate;

            //negative direction
            while (currentX >= 0 && currentY >= 0 && currentX <= MaxX && currentY <= MaxY)
            {
                var existingPoint = AntiNodeLocations.FirstOrDefault(p => p.XCoordinate == currentX && p.YCoordinate == currentY);

                if (existingPoint == null)
                {
                    AntiNodeLocations.Add(new Coordinate() { XCoordinate = currentX, YCoordinate = currentY });

                }
                currentX = currentX - dx;
                currentY = currentY - dy;
            }

        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}

