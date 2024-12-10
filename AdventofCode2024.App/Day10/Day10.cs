using Advent_of_Code_2024.Day10.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.Shared.Models;
using AdventofCode.Core.Shared.Services;
using AdventofCode.Core.Shared.Enum;
using System.Numerics;


namespace Advent_of_Code_2024.Day10
{
    public class Day10 : BasePuzzle
    {
        private readonly CoordinateService _coordinateService;

        private int MaxX { get; set; }
        private int MaxY { get; set; }

        private List<StringValueCoordinate> Grid {  get; set; }

        private List<string> Data { get; set; }

        public Day10(PuzzleTools tools, CoordinateService coordinateService) : base(tools)
        {
            _coordinateService = coordinateService;

            Data = new List<string>();
            Grid = new List<StringValueCoordinate>();
        }

        public async void Challenge1()
        {

            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null || inputData.Map.Count == 0)
            {
                Console.WriteLine("No input data found");
                return;
            }

            Data = inputData.Map;

            MaxY = Data.Count - 1;
            MaxX = Data[0].Length - 1;

            var total = 0;

            List<StringValueCoordinate> startingPoints = new List<StringValueCoordinate>();

            for(int i = 0; i < Data.Count; i++)
            {
                var line = Data[i];
                for (int j = 0; j < line.Length; j++)
                {
                    var gridPoint = new StringValueCoordinate()
                    {
                        Value = line[j].ToString(),
                        XCoordinate = j,
                        YCoordinate = i,
                    };

                    Grid.Add(gridPoint);

                    if (line[j] == '0')
                    {
                        var startingPoint = new StringValueCoordinate()
                        {
                            Value = "0",
                            XCoordinate = j,
                            YCoordinate = i,
                        };

                        startingPoints.Add(startingPoint);
                    }
                }
            }

            foreach (var startingPoint in startingPoints)
            {
                var visitedNines = new List<StringValueCoordinate>();

                FindTrailsRecursive(startingPoint, ref visitedNines);

                if (visitedNines.Count > 0)
                {
                    total+= visitedNines.Count;
                }
            }


            Console.WriteLine($"Solution is {total}");
            return;
        }

        public async void Challenge2()
        {

            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null || inputData.Map.Count == 0)
            {
                Console.WriteLine("No input data found");
                return;
            }

            Data = inputData.Map;

            MaxY = Data.Count - 1;
            MaxX = Data[0].Length - 1;

            var total = 0;

            List<StringValueCoordinate> startingPoints = new List<StringValueCoordinate>();

            for (int i = 0; i < Data.Count; i++)
            {
                var line = Data[i];
                for (int j = 0; j < line.Length; j++)
                {
                    var gridPoint = new StringValueCoordinate()
                    {
                        Value = line[j].ToString(),
                        XCoordinate = j,
                        YCoordinate = i,
                    };

                    Grid.Add(gridPoint);

                    if (line[j] == '0')
                    {
                        var startingPoint = new StringValueCoordinate()
                        {
                            Value = "0",
                            XCoordinate = j,
                            YCoordinate = i,
                        };

                        startingPoints.Add(startingPoint);
                    }
                }
            }

            foreach (var startingPoint in startingPoints)
            {
                var generatedTrails = new List<string>();

                var currentTrail = $"{startingPoint.XCoordinate * 1000 + startingPoint.YCoordinate}";

                GenerateTrailsRecursive(startingPoint, currentTrail, ref generatedTrails);

                if (generatedTrails.Count > 0)
                {
                    total += generatedTrails.Count;
                }
            }


            Console.WriteLine($"Solution is {total}");
            return;
        }

        private async Task<Day10Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day10Model>(10, false).ConfigureAwait(false);
        }

        private void FindTrailsRecursive(StringValueCoordinate currentPoint, ref List<StringValueCoordinate> visitedNines)
        {
            if (currentPoint == null)
                return ;

            if(currentPoint.Value == "9")
            {
                // check if this 9 already visited for this starting point
                var existingVisited = visitedNines.FirstOrDefault(v => v.XCoordinate == currentPoint.XCoordinate && v.YCoordinate == currentPoint.YCoordinate);

                if (existingVisited == null)
                {
                    // if not add to the list
                    visitedNines.Add(currentPoint);
                }
                
                return;
            }

            var nextPoints = new List<Coordinate>();

            var nextUp = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Up, MaxX, MaxY);
            var nextDown = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Down, MaxX, MaxY);
            var nextLeft = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Left, MaxX, MaxY);
            var nextRight = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Right, MaxX, MaxY);

            if(nextUp != null)
            {
                nextPoints.Add(nextUp);
            }

            if (nextDown != null)
            {
                nextPoints.Add(nextDown);
            }

            if (nextLeft != null)
            {
                nextPoints.Add(nextLeft);
            }

            if (nextRight != null)
            {
                nextPoints.Add(nextRight);
            }

            foreach (var point in nextPoints)
            {
                var gridPoint = Grid.FirstOrDefault(p => p.XCoordinate == point.XCoordinate && p.YCoordinate == point.YCoordinate);

                if(gridPoint == null)
                {
                    continue;
                }

                var gridPointValue = int.Parse(gridPoint.Value);
                var currentPointValue = int.Parse(currentPoint.Value);

                if(gridPointValue - currentPointValue != 1)
                {
                    //next space is more than 1 step higher than current, or is going downwards
                    continue;
                }

                // continue on path
                FindTrailsRecursive(gridPoint, ref visitedNines);
            }
        }

        private void GenerateTrailsRecursive(StringValueCoordinate currentPoint, string currentTrail ,ref List<string> generatedTrails)
        {
            if (currentPoint == null)
                return;

            if (currentPoint.Value == "9")
            {
               //check if this path has already been taken
                if (!generatedTrails.Contains(currentTrail))
                {
                    // if not add to the list
                    generatedTrails.Add(currentTrail);
                }

                return;
            }

            var nextPoints = new List<Coordinate>();

            var nextUp = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Up, MaxX, MaxY);
            var nextDown = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Down, MaxX, MaxY);
            var nextLeft = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Left, MaxX, MaxY);
            var nextRight = _coordinateService.GetNextCoordinate(currentPoint, DirectionEnum.Right, MaxX, MaxY);

            if (nextUp != null)
            {
                nextPoints.Add(nextUp);
            }

            if (nextDown != null)
            {
                nextPoints.Add(nextDown);
            }

            if (nextLeft != null)
            {
                nextPoints.Add(nextLeft);
            }

            if (nextRight != null)
            {
                nextPoints.Add(nextRight);
            }

            foreach (var point in nextPoints)
            {
                var gridPoint = Grid.FirstOrDefault(p => p.XCoordinate == point.XCoordinate && p.YCoordinate == point.YCoordinate);

                if (gridPoint == null)
                {
                    continue;
                }

                var gridPointValue = int.Parse(gridPoint.Value);
                var currentPointValue = int.Parse(currentPoint.Value);

                if (gridPointValue - currentPointValue != 1)
                {
                    //next space is more than 1 step higher than current, or is going downwards
                    continue;
                }

                currentTrail += $"-{gridPoint.XCoordinate * 1000 + gridPoint.YCoordinate}";

                // continue on path
                GenerateTrailsRecursive(gridPoint, currentTrail, ref generatedTrails);
            }
        }
    }
}

