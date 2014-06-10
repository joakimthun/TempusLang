using Commons.AST;
using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing.Parselets
{
    public abstract class InfixParselet
    {
        public abstract int GetPrecedence();
        public abstract Expression Parse(Parser parser, Expression left, Token token);
    }
}
