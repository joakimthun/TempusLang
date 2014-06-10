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
    public class VariableDeclarationParselet : StatementParselet
    {
        public override Expression Parse(Parser parser)
        {
            parser.Consume();

            if (!parser.Match(TokenType.Identifier))
            {
                throw new ParsingException(string.Format("Expected identifier, found: {0}", parser.Lookahead.Type));
            }

            var variableDeclaration = new VariableDeclarationExpression();
            variableDeclaration.Identifier = parser.Lookahead.Value;

            parser.Consume();

            if (!parser.Match(TokenType.Assignment))
            {
                throw new ParsingException(string.Format("Expected assignment, found: {0}", parser.Lookahead.Type));
            }

            parser.Consume();

            variableDeclaration.Expression = parser.ParseExpression();

            return variableDeclaration;
        }
    }
}
