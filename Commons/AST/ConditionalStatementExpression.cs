using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class ConditionalStatementExpression : Expression
    {
        public Expression Condition { get; set; }
        public IEnumerable<Expression> IfBody { get; set; }
        public IEnumerable<Expression> ElseBody { get; set; }
    }
}
