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
    public class FuncParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            parser.Consume();

            if (!parser.Match(TokenType.Identifier))
            {
                throw new ParsingException(string.Format("Expected identifier, found: {0}", parser.Lookahead.Type));
            }

            var funcExpression = new FuncDeclarationExpression();
            funcExpression.Name = parser.Lookahead.Value;

            if (funcExpression.Name == "main")
            {
                funcExpression.IsMain = true;
            }

            parser.Consume();

            if (!parser.Match(TokenType.Left_Paren))
            {
                throw new ParsingException(string.Format("Expected opening parentheses, found: {0}", parser.Lookahead.Type));
            }

            var nextToken = parser.Peek(1);

            if (nextToken.Type != TokenType.Right_Paren)
            {
                var arguments = new List<ArgumentExpression>();

                do
                {
                    parser.Consume();

                    if (!parser.Match(TokenType.Identifier))
                    {
                        throw new ParsingException(string.Format("Expected identifier, found: {0}", parser.Lookahead.Type));
                    }

                    var argumentExpression = new ArgumentExpression();
                    argumentExpression.Identifier = parser.Lookahead.Value;

                    parser.Consume();

                    if (!parser.Match(TokenType.Colon))
                    {
                        throw new ParsingException(string.Format("Expected colon, found: {0}", parser.Lookahead.Type));
                    }

                    parser.Consume();

                    if (!parser.Match(TokenType.Identifier))
                    {
                        throw new ParsingException(string.Format("Expected type identifier, found: {0}", parser.Lookahead.Type));
                    }

                    argumentExpression.Type = GetArgumentType(parser.Lookahead);

                    arguments.Add(argumentExpression);

                    parser.Consume();

                }
                while (parser.Match(TokenType.Comma));

                funcExpression.Arguments = arguments;
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

            if (!parser.Match(TokenType.Left_Curly_Bracket))
            {
                throw new ParsingException(string.Format("Expected opening bracket, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            if (!parser.Match(TokenType.Right_Curly_Bracket))
            {
                funcExpression.Body = parser.ParseStatements(TokenType.Right_Curly_Bracket);
            }

            if (!parser.Match(TokenType.Right_Curly_Bracket))
            {
                throw new ParsingException(string.Format("Expected closing bracket, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();


            return funcExpression;
        }

        private Type GetArgumentType(Token token)
        {
            switch (token.Value)
            {
                case "string":
                    return typeof(string);
                case "int":
                    return typeof(int);
                default:
                    throw new ParsingException(string.Format("Unknown type: {0}", token.Value));
            }
        }
    }
}
