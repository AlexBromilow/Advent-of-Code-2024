using Advent_of_Code_2024.Day2.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.Shared.Models;
using System.Runtime.InteropServices;


namespace Advent_of_Code_2024.Day2
{
    public class Day2 : BasePuzzle
    {
        public Day2(PuzzleTools tools) : base(tools)
        {
        }

        public async void Challenge1()
        {
            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            if (inputData.Reports.Count == 0)
            {
                Console.WriteLine("Input data is empty");
                return;
            }

            int safeCount = 0;

            foreach (var report in inputData.Reports)
            {
                var differences = new List<int>();

                for (int i = 0; i < report.Values.Count - 1; i++)
                {
                    differences.Add(report.Values[i] - report.Values[i + 1]);
                }

                if (EvaluateInput(report.Values))
                {
                    safeCount++;
                };
            }


            Console.WriteLine($"Solution is {safeCount}");
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

            if (inputData.Reports.Count == 0)
            {
                Console.WriteLine("Input data is empty");
                return;
            }

            int safeCount = 0;

            foreach (var report in inputData.Reports)
            {
                var differences = new List<int>();

                for (int i = 0; i < report.Values.Count - 1; i++)
                {
                    differences.Add(report.Values[i] - report.Values[i + 1]);
                }

                if (EvaluateInput(report.Values, true))
                {
                    safeCount++;
                };
            }


            Console.WriteLine($"Solution is {safeCount}");
            return;
        }

        private async Task<Day2Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day2Model>(2, false).ConfigureAwait(false);
        }

        private bool EvaluateInput(List<int> input, bool allowRecursion = false)
        {
            var isSafe = true;

            var sortedInput = new List<int>();
            var reverseSortedInput = new List<int>();
            sortedInput.AddRange(input);
            reverseSortedInput.AddRange(input);

            sortedInput.Sort();
            reverseSortedInput.Sort();
            reverseSortedInput.Reverse();

            //If input is not strictly increasing or decreasing, not safe
            if (!input.SequenceEqual(sortedInput) && !input.SequenceEqual(reverseSortedInput))
            {
                isSafe = false;
            }

            for (int i = 0; i < input.Count - 1; i++)
            {
                var difference = Math.Abs(input[i] - input[i + 1]);

                if (difference < 1 || difference > 3)
                {
                    isSafe = false;
                    break;
                }
            }

            if (!isSafe)
            {
                if(!allowRecursion)
                {
                    return false;
                }

                for (int i = 0; i < input.Count; i++)
                {
                    var workingValues = new List<int>();
                    workingValues.AddRange(input);
                    workingValues.RemoveAt(i);

                    if (EvaluateInput(workingValues))
                    {
                        return true;
                    };
                }

                return false;
            }

            return true;
        }
    }
}
