using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class StringLiteralExpression : Expression
    {
        public string Value { get; set; }
    }
}
