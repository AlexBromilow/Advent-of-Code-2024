using AdventofCode.Core.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2024.Day5.Model
{
    public class Day5Model
    {
        public Day5Model()
        {
            Rules = [];
            Updates = [];
        }

        public List<OrderRules> Rules { get; set; }

        public List<List<int>> Updates { get; set; }
    }

    public class  OrderRules
    {
        public int FirstNumber { get;set; }
        public int SecondNumber { get;set; }
        
    }
}
