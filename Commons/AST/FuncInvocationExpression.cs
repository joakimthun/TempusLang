using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class FuncInvocationExpression : Expression
    {
        public string Name { get; set; }
        public IEnumerable<Expression> Arguments { get; set; }
    }
}
