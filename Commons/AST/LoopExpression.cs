using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class LoopExpression : Expression
    {
        public Expression IterationExpression { get; set; }
        public IEnumerable<Expression> Body { get; set; }
    }
}
