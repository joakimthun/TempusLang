using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class PrefixUnaryExpression : Expression
    {
        public TokenType Operator { get; set; }
        public Expression Right { get; set; }
    }
}
