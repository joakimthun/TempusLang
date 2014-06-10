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
    public class AssignmentParselet: InfixParselet
    {
        public override int GetPrecedence()
        {
            return Precedence.Assignment;
        }

        public override Expression Parse(Parser parser, Expression left, Token token)
        {
            parser.Consume();

            var right = parser.ParseExpression(Precedence.Assignment - 1);

            var identifierExpression = left as IdentifierExpression;
            if(identifierExpression != null)
                return new AssignmentExpression { Identifier = identifierExpression.Identifier, Expression = right };

            throw new ParsingException("The left hand side of an expression must be an idenfier or a variable declaration.");
        }
    }
}
