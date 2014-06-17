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
    public class FuncInvocationParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            var funcInvocationExpression = new FuncInvocationExpression();
            funcInvocationExpression.Name = parser.Lookahead.Value;

            parser.Consume();

            if (!parser.Match(TokenType.Left_Paren))
            {
                throw new ParsingException(string.Format("Expected opening parentheses, found: {0}", parser.Lookahead.Type));
            }

            var nextToken = parser.Peek(1);

            if (nextToken.Type != TokenType.Right_Paren)
            {
                var arguments = new List<Expression>();

                do
                {
                    parser.Consume();

                    if (parser.Match(TokenType.Identifier))
                    {
                        arguments.Add(new IdentifierExpression { Identifier = parser.Lookahead.Value });
                        parser.Consume();
                    }
                    else
                    {
                        arguments.Add(parser.ParseExpression());
                    }
                }
                while (parser.Match(TokenType.Comma));

                funcInvocationExpression.Arguments = arguments;
            }
            else
            {
                parser.Consume();
            }

            if (!parser.Match(TokenType.Right_Paren))
            {
                throw new ParsingException(string.Format("Expected closing parentheses, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();


            return funcInvocationExpression;
        }
    }
}
