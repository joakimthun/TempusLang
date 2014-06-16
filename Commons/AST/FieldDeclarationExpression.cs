using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class FieldDeclarationExpression : Expression
    {
        public string Identifier { get; set; }
        public Type Type { get; set; }
    }
}
