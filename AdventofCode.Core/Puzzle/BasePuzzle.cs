using AdventofCode.Core.PuzzleInput.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Core.Puzzle
{
    public abstract class BasePuzzle
    {
        protected IPuzzleInputService PuzzleInputService;

        protected BasePuzzle(PuzzleTools tools)
        {
            PuzzleInputService = tools.PuzzleInputService;

        }
    }
}
