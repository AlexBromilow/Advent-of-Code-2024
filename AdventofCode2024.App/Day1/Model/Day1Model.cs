using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day1.Model
{
    public class Day1Model
    {
        public Day1Model()
        {
            LeftColumn = [];
            RightColumn = [];
        }

        public List<int> LeftColumn { get; set; }
        public List<int> RightColumn { get; set; }
    }
}
