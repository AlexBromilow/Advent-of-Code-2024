using AdventofCode.Core.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day7.Model
{
    public class Day7Model
    {
        public Day7Model()
        {
            Equations = [];
        }

        public List<Equation> Equations { get; set; }
    }

    public class Equation
    {
        public Equation()
        {
            Values = [];
            TestValue = string.Empty;
        }

        public string TestValue { get; set; }

        public List<string> Values { get; set; }
    }
}
