using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode.Core.Shared.Models
{
    public class NumberList
    {
        public NumberList() {
            Values = [];
        }
        public List<int> Values { get; set; }
    }
}
