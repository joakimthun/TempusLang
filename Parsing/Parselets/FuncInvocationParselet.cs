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

            parser.Consume();

            if (parser.Match(TokenType.Identifier))
            {
                var arguments = new List<IdentifierExpression>();
                arguments.Add(new IdentifierExpression { Identifier = parser.Lookahead.Value });
                parser.Consume();

                while (parser.Match(TokenType.Comma))
                {
                    parser.Consume();

                    if (!parser.Match(TokenType.Identifier))
                    {
                        throw new ParsingException(string.Format("Expected identifier, found: {0}", parser.Lookahead.Type));
                    }

                    arguments.Add(new IdentifierExpression { Identifier = parser.Lookahead.Value });

                    parser.Consume();
                }

                funcInvocationExpression.Arguments = arguments;
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
