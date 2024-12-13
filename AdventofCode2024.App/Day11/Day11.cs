using Advent_of_Code_2024.Day11.Model;
using AdventofCode.Core.Puzzle;
using System.Numerics;


namespace Advent_of_Code_2024.Day11
{
    public class Day11 : BasePuzzle
    {

        public Dictionary<BigInteger, BigInteger> StoneTracker { get; set; }
        public Dictionary<BigInteger, BigInteger> WorkingTracker { get; set; }

        public Day11(PuzzleTools tools) : base(tools)
        {
            StoneTracker = new Dictionary<BigInteger, BigInteger>();
            WorkingTracker = new Dictionary<BigInteger, BigInteger>();
        }

        public async void Challenge1()
        {
            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null || inputData.Starting.Count == 0)
            {
                Console.WriteLine("No input data found");
                return;
            }

            BigInteger total = 0;

            foreach (var item in inputData.Starting)
            {
                if (StoneTracker.ContainsKey(item))
                {
                    StoneTracker[item]++;
                }
                else
                {
                    StoneTracker[item] = 1;
                }
            }

            for (int i = 0; i < 25; i++)
            {
                WorkingTracker = new Dictionary<BigInteger, BigInteger>();

                foreach (var stoneValue in StoneTracker.Keys)
                {
                    var stoneCount = StoneTracker[stoneValue];

                    // if no stones of this value, skip to next
                    if (stoneCount == 0)
                    { 
                        continue; 
                    }

                    // turn all zeros to 1's
                    if(stoneValue == 0)
                    {
                        AddStones(1, stoneCount);
                        continue;
                    }

                    var stringStoneValue = stoneValue.ToString();

                    // check for even number of digits
                    if(stringStoneValue.Length % 2 == 0)
                    {
                        var firstHalf = stringStoneValue.Substring(0, stringStoneValue.Length/ 2);
                        var secondHalf = stringStoneValue.Substring(stringStoneValue.Length / 2, stringStoneValue.Length/ 2);

                        AddStones(BigInteger.Parse(firstHalf), stoneCount);
                        AddStones(BigInteger.Parse(secondHalf), stoneCount);
                        continue;
                    }

                    AddStones(stoneValue * 2024, stoneCount);
                }

                StoneTracker = WorkingTracker;
            }

            foreach (var stoneValue in StoneTracker.Values)
            {
                total += stoneValue;
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        public async void Challenge2()
        {

            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null || inputData.Starting.Count == 0)
            {
                Console.WriteLine("No input data found");
                return;
            }

            BigInteger total = 0;

            foreach (var item in inputData.Starting)
            {
                if (StoneTracker.ContainsKey(item))
                {
                    StoneTracker[item]++;
                }
                else
                {
                    StoneTracker[item] = 1;
                }
            }

            for (int i = 0; i < 75; i++)
            {
                WorkingTracker = new Dictionary<BigInteger, BigInteger>();

                foreach (var stoneValue in StoneTracker.Keys)
                {
                    var stoneCount = StoneTracker[stoneValue];

                    // if no stones of this value, skip to next
                    if (stoneCount == 0)
                    {
                        continue;
                    }

                    // turn all zeros to 1's
                    if (stoneValue == 0)
                    {
                        AddStones(1, stoneCount);
                        continue;
                    }

                    var stringStoneValue = stoneValue.ToString();

                    // check for even number of digits
                    if (stringStoneValue.Length % 2 == 0)
                    {
                        var firstHalf = stringStoneValue.Substring(0, stringStoneValue.Length / 2);
                        var secondHalf = stringStoneValue.Substring(stringStoneValue.Length / 2, stringStoneValue.Length / 2);

                        AddStones(BigInteger.Parse(firstHalf), stoneCount);
                        AddStones(BigInteger.Parse(secondHalf), stoneCount);
                        continue;
                    }

                    AddStones(stoneValue * 2024, stoneCount);
                }

                StoneTracker = WorkingTracker;
            }

            foreach (var stoneValue in StoneTracker.Values)
            {
                total += stoneValue;
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        private async Task<Day11Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day11Model>(11, false).ConfigureAwait(false);
        }

        private void AddStones(BigInteger stoneValue, BigInteger stoneCount)
        {
            if (WorkingTracker.ContainsKey(stoneValue))
            {
                WorkingTracker[stoneValue] += stoneCount;
            }
            else
            {
                WorkingTracker[stoneValue] = stoneCount;
            }
        }
    }
}

