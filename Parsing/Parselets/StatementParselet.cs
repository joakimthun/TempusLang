using Commons.AST;
using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing.Parselets
{
    public abstract class StatementParselet
    {
        public abstract Expression Parse(Parser parser);
    }
}
