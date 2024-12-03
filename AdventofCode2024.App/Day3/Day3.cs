using Advent_of_Code_2024.Day3.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.Shared.Models;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace Advent_of_Code_2024.Day3
{
    public class Day3 : BasePuzzle
    {
        public Day3(PuzzleTools tools) : base(tools)
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

            var instructionRegex = "mul\\((\\d+,\\d+)\\)";
            var total = 0;

            var matches = Regex.Matches(inputData.Instructions, instructionRegex, RegexOptions.IgnoreCase);

            if (matches.Count == 0)
            {
                Console.WriteLine($"Solution is 0");
            }

            foreach (Match match in matches)
            {
                var values = match.Groups[1].Value.Split(',');

                if (values.Length != 2)
                {
                    Console.WriteLine($"Something went wrong");
                    return;
                }

                var firstNumber = int.Parse(values[0]);
                var secondNumber = int.Parse(values[1]);

                total += firstNumber * secondNumber;
            }

            Console.WriteLine($"Solution is {total}");
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

            var instructionRegex = "mul\\((\\d+,\\d+)\\)";
            var doRegex = "do\\(\\)";
            var dontRegex = "don't\\(\\)";

            var total = 0;

            var instructionMatches = Regex.Matches(inputData.Instructions, instructionRegex, RegexOptions.IgnoreCase);
            var doMatches = Regex.Matches(inputData.Instructions, doRegex, RegexOptions.IgnoreCase);
            var dontMatches = Regex.Matches(inputData.Instructions, dontRegex, RegexOptions.IgnoreCase);

            if (instructionMatches.Count == 0)
            {
                Console.WriteLine($"Solution is 0");
            }

            var firstDontIndex = dontMatches.First().Index;

            foreach (Match match in instructionMatches)
            {
                var enabled = true;
                if (match.Index >= firstDontIndex)
                {
                    enabled = false;
                }

                //if after the first don't, check for nearest do
                if (!enabled)
                {
                    // get the do and the don't closest to the current match from the left
                    var closestDo = doMatches.LastOrDefault(x => x.Index <= match.Index);
                    var closestDont = dontMatches.LastOrDefault(x => x.Index <= match.Index);

                    if(closestDo != null && closestDont != null)
                    {
                        // if the 'do' is closer than the 'don't', enable the mul
                        if ((match.Index - closestDo.Index) < (match.Index - closestDont.Index))
                        {
                            enabled = true;
                        }
                    }
                    
                }

                if (enabled)
                {
                    var values = match.Groups[1].Value.Split(',');

                    if (values.Length != 2)
                    {
                        Console.WriteLine($"Something went wrong");
                        return;
                    }

                    var firstNumber = int.Parse(values[0]);
                    var secondNumber = int.Parse(values[1]);

                    total += firstNumber * secondNumber;
                }
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        private async Task<Day3Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day3Model>(3, false).ConfigureAwait(false);
        }


    }
}
