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
    public class LoopParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            parser.Consume();

            if (!parser.Match(TokenType.Left_Paren))
            {
                throw new ParsingException(string.Format("Expected opening parentheses, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            var loopExpression = new LoopExpression();
            loopExpression.IterationExpression = parser.ParseExpression();

            if (!parser.Match(TokenType.Right_Paren))
            {
                throw new ParsingException(string.Format("Expected closing parentheses, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            if (!parser.Match(TokenType.Left_Curly_Bracket))
            {
                throw new ParsingException(string.Format("Expected opening bracket, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            if (!parser.Match(TokenType.Right_Curly_Bracket))
            {
                loopExpression.Body = parser.ParseStatements(TokenType.Right_Curly_Bracket);
            }

            if (!parser.Match(TokenType.Right_Curly_Bracket))
            {
                throw new ParsingException(string.Format("Expected closing bracket, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            return loopExpression;
        }
    }
}
