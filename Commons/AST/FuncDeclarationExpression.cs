using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class FuncDeclarationExpression : Expression
    {
        public string Name { get; set; }
        public IEnumerable<ArgumentExpression> Arguments { get; set; }
        public IEnumerable<Expression> Body { get; set; }
        public bool IsMain { get; set; }
    }
}
