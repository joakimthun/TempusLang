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
    public class PrintLineParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            parser.Consume();

            var printLineExpression = new PrintLineExpression();
            printLineExpression.Expression = parser.ParseExpression();

            return printLineExpression;
        }
    }
}
