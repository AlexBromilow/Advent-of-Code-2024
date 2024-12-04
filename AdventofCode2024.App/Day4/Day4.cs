using Advent_of_Code_2024.Day4.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.Shared.Enum;
using AdventofCode.Core.Shared.Models;
using AdventofCode.Core.Shared.Services;



namespace Advent_of_Code_2024.Day4
{
    public class Day4 : BasePuzzle
    {
        private readonly CoordinateService _coordinateService;

        private readonly Dictionary<string, string> letterPattern = new Dictionary<string, string>() {
            {"X", "M" },
            {"M", "A" },
            {"A", "S" },
            {"S", "Y" },
        };

        private List<string> Data = [];

        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public Day4(PuzzleTools tools, CoordinateService coordinateService) : base(tools)
        {
            _coordinateService = coordinateService;
        }

        public async void Challenge1()
        {
            var inputData = await GetData().ConfigureAwait(false);

            var total = 0;
            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            if (inputData.Lines == null || inputData.Lines.Count == 0)
            {
                Console.WriteLine("Something went wrong");
                return;
            }

            Data = inputData.Lines;

            MaxX = Data[0].Length - 1;
            MaxY = Data.Count - 1;

            var xPlaces = new List<StringValueCoordinate>();

            for (var i = 0; i < Data.Count; i++)
            {
                var line = Data[i];

                for (var j = 0; j < line.Length; j++)
                {
                    var currentCharacter = line[j];
                    if (currentCharacter == 'X')
                    {
                        xPlaces.Add(new StringValueCoordinate
                        {
                            Value = "X",
                            XCoordinate = j,
                            YCoordinate = i
                        });
                    }
                }

            }

            foreach (var x in xPlaces)
            {
                if (FindPath(x.Value, x, DirectionEnum.Left))
                {
                    total++;
                };

                if (FindPath(x.Value, x, DirectionEnum.Right))
                {
                    total++;
                };

                if (FindPath(x.Value, x, DirectionEnum.Up))
                {
                    total++;
                };

                if (FindPath(x.Value, x, DirectionEnum.Down))
                {
                    total++;
                };

                if (FindPath(x.Value, x, DirectionEnum.NE))
                {
                    total++;
                };

                if (FindPath(x.Value, x, DirectionEnum.NW))
                {
                    total++;
                };

                if (FindPath(x.Value, x, DirectionEnum.SE))
                {
                    total++;
                };

                if (FindPath(x.Value, x, DirectionEnum.SW))
                {
                    total++;
                };
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        public async void Challenge2()
        {
            var inputData = await GetData().ConfigureAwait(false);
            var total = 0;
            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            if (inputData.Lines == null || inputData.Lines.Count == 0)
            {
                Console.WriteLine("Something went wrong");
                return;
            }

            Data = inputData.Lines;

            MaxX = Data[0].Length - 1;
            MaxY = Data.Count - 1;

            var aPlaces = new List<StringValueCoordinate>();

            for (var i = 0; i < Data.Count; i++)
            {
                var line = Data[i];

                for (var j = 0; j < line.Length; j++)
                {
                    var currentCharacter = line[j];
                    if (currentCharacter == 'A')
                    {
                        aPlaces.Add(new StringValueCoordinate
                        {
                            Value = "A",
                            XCoordinate = j,
                            YCoordinate = i
                        });
                    }
                }

            }

            foreach (var a in aPlaces)
            {
                // for every A, check corners for diagonal 'mas'

                // if first check failes, skip to next A
                if(!CheckForMas(a, DirectionEnum.NE, DirectionEnum.SW))
                {
                    continue;
                }

                // check the other diagonal to find X-mas
                if(CheckForMas(a, DirectionEnum.NW, DirectionEnum.SE))
                {
                    total++;
                }
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        private async Task<Day4Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day4Model>(4, false).ConfigureAwait(false);
        }

        private bool FindPath(string currentValue, Coordinate currentCoordinate, DirectionEnum direction)
        {
            var nextCharacter = letterPattern.FirstOrDefault(x => x.Key == currentValue).Value;

            if (string.IsNullOrEmpty(nextCharacter))
            {
                return false;
            }

            if (nextCharacter == "Y")
            {
                return true;
            }

            var nextCoordinate = _coordinateService.GetNextCoordinate(currentCoordinate, direction, MaxX, MaxY);

            if (nextCoordinate == null)
            {
                return false;
            }

            var checkCharacter = Data[nextCoordinate.YCoordinate][nextCoordinate.XCoordinate].ToString();

            if (string.IsNullOrEmpty(checkCharacter))
            {
                return false;
            }

            if (checkCharacter == nextCharacter)
            {
                return FindPath(nextCharacter, nextCoordinate, direction);
            }

            return false;
        }

        private bool CheckForMas(Coordinate currentCoordinate, DirectionEnum firstDirection, DirectionEnum secondDirection)
        {
            var firstDirectionCoordinate = _coordinateService.GetNextCoordinate(currentCoordinate, firstDirection, MaxX, MaxY);
            var secondDirectionCoordinate = _coordinateService.GetNextCoordinate(currentCoordinate, secondDirection, MaxX, MaxY);

            if (firstDirectionCoordinate == null || secondDirectionCoordinate == null)
            {
                return false;
            }

            var firstCheckCharacter = Data[firstDirectionCoordinate.YCoordinate][firstDirectionCoordinate.XCoordinate].ToString();
            var secondCheckCharacter = Data[secondDirectionCoordinate.YCoordinate][secondDirectionCoordinate.XCoordinate].ToString();

            // If opposing letters are M and S, we have a 'mas', return true
            if (firstCheckCharacter == "M" && secondCheckCharacter == "S")
            {
                return true;
            }
            else if (firstCheckCharacter == "S" && secondCheckCharacter == "M")
            {
                return true;
            }

            return false;
        }
    }
}
