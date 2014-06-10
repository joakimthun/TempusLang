using Commons.AST;
using Commons.Exceptions;
using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing.Parselets
{
    public class ReturnParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            parser.Consume();

            var returnExpression = new ReturnExpression();
            returnExpression.Expression = parser.ParseExpression();

            return returnExpression;
        }
    }
}
