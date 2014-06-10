using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class PrintLineExpression : Expression
    {
        public Expression Expression { get; set; }
    }
}
