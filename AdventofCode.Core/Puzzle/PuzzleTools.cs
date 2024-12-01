using AdventofCode.Core.PuzzleInput.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Core.Puzzle
{
    public class PuzzleTools
    {
        public IPuzzleInputService PuzzleInputService { get; }

        public PuzzleTools(IPuzzleInputService puzzleInputService)
        {
            PuzzleInputService = puzzleInputService;
        }
    }
}
