using Commons.AST;
using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing.Parselets
{
    public class IntegerLiteralParselet : PrefixParselet
    {
        public override Expression Parse(Parser parser, Token token)
        {
            var integerLiteralExpression = new IntegerLiteralExpression { Value = int.Parse(token.Value) };
            parser.Consume();

            return integerLiteralExpression;
        }
    }
}
