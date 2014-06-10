using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class AssignmentExpression : Expression
    {
        public string Identifier { get; set; }
        public Expression Expression { get; set; }
    }
}
