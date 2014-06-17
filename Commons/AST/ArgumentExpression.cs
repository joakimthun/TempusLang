using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class ArgumentExpression : Expression
    {
        public Type Type { get; set; }
        public string Identifier { get; set; }
    }
}
