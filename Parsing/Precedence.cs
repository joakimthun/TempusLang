using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing.Parselets
{
    public static class Precedence
    {
        public const int Assignment = 1;
        public const int Comparison = 2;
        public const int Sum = 3;
        public const int Product = 4;
    }
}
