using Advent_of_Code_2024.Day5.Model;
using AdventofCode.Core.Puzzle;

namespace Advent_of_Code_2024.Day5
{
    public class Day5 : BasePuzzle
    {
        private List<OrderRules> Rules = new List<OrderRules>();

        public Day5(PuzzleTools tools) : base(tools)
        {
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

            Rules.AddRange(inputData.Rules);

            foreach (var update in inputData.Updates)
            {
                var workingList = new List<int>();
                workingList.AddRange(update);

                BubbleSort(ref workingList);

                if (workingList.SequenceEqual(update))
                {
                    var middleNumberIndex = (int)Math.Floor((decimal)update.Count / 2);

                    total += update[middleNumberIndex];
                }
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

            Rules.AddRange(inputData.Rules);

            foreach (var update in inputData.Updates)
            {
                var workingList = new List<int>();
                workingList.AddRange(update);

                BubbleSort(ref workingList);

                if (!workingList.SequenceEqual(update))
                {
                    var middleNumberIndex = (int)Math.Floor((decimal)workingList.Count / 2);

                    total += workingList[middleNumberIndex];
                }
            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        private async Task<Day5Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day5Model>(5, false).ConfigureAwait(false);
        }

        private void BubbleSort(ref List<int> list)
        {

            for (int i = 0; i < list.Count - 1; i++)
            {
                var numberOfSwaps = 0;

                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    var firstValue = list[j];
                    var secondValue = list[j + 1];
                    var orderRule = Rules.FirstOrDefault(x => x.FirstNumber == secondValue && x.SecondNumber == firstValue);

                    if (orderRule == null)
                    {
                        continue;
                    }

                    int temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                    numberOfSwaps++;

                }

                //If a full pass with no swaps happens, numbers in correct order;
                if (numberOfSwaps == 0)
                {
                    break;
                }
            }

            return;
        }

    }
}
