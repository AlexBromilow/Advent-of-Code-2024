using Advent_of_Code_2024.Day6.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.Shared.Enum;
using AdventofCode.Core.Shared.Models;
using AdventofCode.Core.Shared.Services;
using System.Runtime.CompilerServices;

namespace Advent_of_Code_2024.Day6
{
    public class Day6 : BasePuzzle
    {
        public readonly CoordinateService _coordinateService;

        public List<string> Data;

        public int MaxX;
        public int MaxY;

        public List<int> VisitedLocations = new List<int>();
        public List<string> ObstacleLocations = new List<string>();

        public Day6(PuzzleTools tools, CoordinateService coordinateService) : base(tools)
        {
            _coordinateService = coordinateService;
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

            StringValueCoordinate startingPosition = new StringValueCoordinate();

            for (var i = 0; i < Data.Count; i++)
            {
                var line = Data[i];

                if (line.Contains("^"))
                {
                    startingPosition.YCoordinate = i;
                    startingPosition.XCoordinate = line.IndexOf("^");
                    startingPosition.Value = "^";
                }
            }

            var leftArea = false;

            var currentPosition = new StringValueCoordinate()
            {
                XCoordinate = startingPosition.XCoordinate,
                YCoordinate = startingPosition.YCoordinate,
                Value = startingPosition.Value
            };

            while (!leftArea)
            {
                var positionId = (currentPosition.XCoordinate * 1000) + currentPosition.YCoordinate;

                if (!VisitedLocations.Contains(positionId))
                {
                    VisitedLocations.Add(positionId);
                }

                ProcessPath(ref currentPosition, ref leftArea);
            }

            Console.WriteLine($"Solution is {VisitedLocations.Count}");
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

            var loopCount = 0;
            Data = inputData.Lines;

            MaxX = Data[0].Length - 1;
            MaxY = Data.Count - 1;

            StringValueCoordinate startingPosition = new StringValueCoordinate();

            for (var i = 0; i < Data.Count; i++)
            {
                var line = Data[i];

                if (line.Contains("^"))
                {
                    startingPosition.YCoordinate = i;
                    startingPosition.XCoordinate = line.IndexOf("^");
                    startingPosition.Value = "^";
                }
            }

            var leftArea = false;

            var currentPosition = new StringValueCoordinate()
            {
                XCoordinate = startingPosition.XCoordinate,
                YCoordinate = startingPosition.YCoordinate,
                Value = startingPosition.Value
            };

            //start by finding all the places visited in initial run
            while (!leftArea)
            {
                var positionId = (currentPosition.XCoordinate * 1000) + currentPosition.YCoordinate;

                if (!VisitedLocations.Contains(positionId))
                {
                    VisitedLocations.Add(positionId);
                }

                ProcessPath(ref currentPosition, ref leftArea);
            }

            //change each visited location to an obstruction and check for a loop
            foreach (var visitedLocation in VisitedLocations)
            {
                //reset locations on each run through
                ObstacleLocations = new List<string>();
                currentPosition.XCoordinate = startingPosition.XCoordinate;
                currentPosition.YCoordinate = startingPosition.YCoordinate;
                currentPosition.Value = startingPosition.Value;

                leftArea = false;

                var locationY = visitedLocation % 1000;
                var locationX = (visitedLocation - locationY) / 1000;

                if (locationX == startingPosition.XCoordinate && locationY == startingPosition.YCoordinate)
                {
                    continue;
                }

                // change specified point to obstruction
                Data[locationY] = Data[locationY].Remove(locationX, 1).Insert(locationX, "#");

                while (!leftArea)
                {
                    var positionId = $"{(currentPosition.XCoordinate * 1000) + currentPosition.YCoordinate}-{currentPosition.Value}";

                    if (ObstacleLocations.Contains(positionId))
                    {
                        loopCount++;
                        break;
                    }
                    else
                    {
                        ObstacleLocations.Add(positionId);
                    }

                    ProcessPath(ref currentPosition, ref leftArea);
                }

                // at the end of each pass, reset data to starting conditions
                Data[locationY] = Data[locationY].Remove(locationX, 1).Insert(locationX, ".");
            }

            Console.WriteLine($"Solution is {loopCount}");
            return;
        }

        private async Task<Day6Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day6Model>(6, false).ConfigureAwait(false);
        }

        private void ProcessPath(ref StringValueCoordinate currentPosition, ref bool leftArea)
        {
            DirectionEnum direction = DirectionEnum.Up;

            switch (currentPosition.Value)
            {
                case ">":
                    direction = DirectionEnum.Right;
                    break;
                case "<":
                    direction = DirectionEnum.Left;
                    break;
                case "V":
                    direction = DirectionEnum.Down;
                    break;
            }

            var nextPosition = _coordinateService.GetNextCoordinate(currentPosition, direction, MaxX, MaxY);

            if (nextPosition == null)
            {
                leftArea = true;
                return;
            }

            var nextPoint = Data[nextPosition.YCoordinate][nextPosition.XCoordinate];

            if (nextPoint == '#')
            {
                currentPosition = Rotate90(currentPosition);
            }
            else
            {
                currentPosition.XCoordinate = nextPosition.XCoordinate;
                currentPosition.YCoordinate = nextPosition.YCoordinate;
            }

        }

        private StringValueCoordinate Rotate90(StringValueCoordinate currentPosition)
        {
            var newCoordinate = new StringValueCoordinate()
            {
                XCoordinate = currentPosition.XCoordinate,
                YCoordinate = currentPosition.YCoordinate
            };

            switch (currentPosition.Value)
            {
                case "^":
                    newCoordinate.Value = ">";
                    break;
                case ">":
                    newCoordinate.Value = "V";
                    break;
                case "<":
                    newCoordinate.Value = "^";
                    break;
                case "V":
                    newCoordinate.Value = "<";
                    break;
            }

            return newCoordinate;
        }
    }
}
