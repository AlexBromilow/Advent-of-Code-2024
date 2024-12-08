using Advent_of_Code_2024.Day7.Model;
using AdventofCode.Core.Puzzle;
using AdventofCode.Core.Shared.Enum;
using AdventofCode.Core.Shared.Models;
using AdventofCode.Core.Shared.Services;
using System.CodeDom.Compiler;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Advent_of_Code_2024.Day7
{
    public class Day7 : BasePuzzle
    {
        public List<Equation> Data { get; set; }

        public Day7(PuzzleTools tools) : base(tools)
        {
            Data = new List<Equation>();
        }


        public async void Challenge1()
        {

            var inputData = await GetData().ConfigureAwait(false);

            if (inputData == null)
            {
                Console.WriteLine("No input data found");
                return;
            }

            Data = inputData.Equations;

            BigInteger total = 0;

            foreach (Equation eq in Data)
            {
                if(!BigInteger.TryParse(eq.TestValue, out BigInteger testValue))
                {
                    continue;
                }

                var values = eq.Values.Select(x => BigInteger.Parse(x)).ToList();

                if (EvaluateRecursively(testValue, values, 0, values[0]))
                {
                    total += testValue;
                }

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

            Data = inputData.Equations;

            BigInteger total = 0;

            foreach (Equation eq in Data)
            {
                if (!BigInteger.TryParse(eq.TestValue, out BigInteger testValue))
                {
                    continue;
                }

                var values = eq.Values.Select(x => BigInteger.Parse(x)).ToList();

                if (EvaluateRecursively(testValue, values, 0, values[0], true))
                {
                    total += testValue;
                }

            }

            Console.WriteLine($"Solution is {total}");
            return;
        }

        private async Task<Day7Model?> GetData()
        {
            return await PuzzleInputService.GetPuzzleInput<Day7Model>(7, false).ConfigureAwait(false);
        }

        private bool EvaluateRecursively(BigInteger target, List<BigInteger> values, int index, BigInteger currentResult, bool allowConcat = false)
        {
            if (index == values.Count - 1)
            {
                if (currentResult == target)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //if we're over the target before the end, this combination doesn't work
            if (currentResult > target)
            {
                return false;
            }

            int nextIndex = index + 1;

            // try addition 
            if (EvaluateRecursively(target, values, nextIndex, currentResult + values[nextIndex], allowConcat))
            {
                return true;
            };


            // try multiplication
            if (EvaluateRecursively(target, values, nextIndex, currentResult * values[nextIndex], allowConcat))
            {
                return true;
            }

            if (allowConcat && EvaluateRecursively(target, values, nextIndex, BigInteger.Parse($"{currentResult}{values[nextIndex]}"), allowConcat))
            {
                return true;
            }

            return false;
        }

    }
}
