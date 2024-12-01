using Advent_of_Code_2024.Day1.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.PuzzleInput.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day1
{
    public class Day1 : BasePuzzle
    {
        public Day1(PuzzleTools tools) : base(tools)
        {
        }

        public async void Challenge1()
        {
            var inputData = await GetData().ConfigureAwait(false);

            if(inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            if(inputData.LeftColumn.Count == 0 || inputData.RightColumn.Count == 0)
            {
                Console.WriteLine("Input data is empty");
                return;
            }

            int total = 0;

            inputData.LeftColumn.Sort();
            inputData.RightColumn.Sort();

            for(int i = 0; i < inputData.LeftColumn.Count;i++)
            {
                var leftValue = inputData.LeftColumn[i];
                var rightValue = inputData.RightColumn[i];

                var difference = Math.Abs(leftValue - rightValue);

                total += difference;
            }

            Console.WriteLine($"Solution is: {total}");
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

            if (inputData.LeftColumn.Count == 0 || inputData.RightColumn.Count == 0)
            {
                Console.WriteLine("Input data is empty");
                return;
            }

            int total = 0;

            foreach(var item in inputData.LeftColumn)
            {
                var timesInRight = inputData.RightColumn.Where(x => x == item).Count();

                total += item * timesInRight;
            }

            Console.WriteLine($"Solution is: {total}");
            return;
        }

        private async Task<Day1Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day1Model>(1, false).ConfigureAwait(false);
        }

    }
}
