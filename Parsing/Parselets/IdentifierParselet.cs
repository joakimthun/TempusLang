using Commons.AST;
using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing.Parselets
{
    public class IdentifierParselet : PrefixParselet
    {
        public override Expression Parse(Parser parser, Token token)
        {
            var identifierExpression = new IdentifierExpression { Identifier = token.Value };
            parser.Consume();

            return identifierExpression;
        }
    }
}
