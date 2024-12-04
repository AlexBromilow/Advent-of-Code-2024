using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Core.Shared.Models
{
    public class StringValueCoordinate : Coordinate
    {
        public StringValueCoordinate()
        {
            Value = string.Empty;
        }
        public string Value { get; set; }
    }
}
