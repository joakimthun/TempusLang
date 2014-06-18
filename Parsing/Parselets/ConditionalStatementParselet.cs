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
    public class ConditionalStatementParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            parser.Consume();

            if (!parser.Match(TokenType.Left_Paren))
            {
                throw new ParsingException(string.Format("Expected opening parentheses, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            var conditionalStatementExpression = new ConditionalStatementExpression();

            throw new ParsingException("Conditional statements are not yet supported :(");

            return conditionalStatementExpression;
        }
    }
}
