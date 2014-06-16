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
    public class FieldDeclarationParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            parser.Consume();

            if (!parser.Match(TokenType.Identifier))
            {
                throw new ParsingException(string.Format("Expected identifier, found: {0}", parser.Lookahead.Type));
            }

            var fieldDeclarationExpression = new FieldDeclarationExpression();
            fieldDeclarationExpression.Identifier = parser.Lookahead.Value;

            parser.Consume();

            if (!parser.Match(TokenType.Colon))
            {
                throw new ParsingException(string.Format("Expected assignment, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            fieldDeclarationExpression.Type = GetFieldType(parser.Lookahead);

            parser.Consume();

            return fieldDeclarationExpression;
        }

        private Type GetFieldType(Token token)
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
