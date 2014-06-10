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
    public class BinaryOperatorParselet: InfixParselet
    {
        private int _precedence;
        private bool _isRightAssociative;

        public BinaryOperatorParselet(int precedence, bool isRightAssociative)
        {
            _precedence = precedence;
            _isRightAssociative = isRightAssociative;
        }

        public override int GetPrecedence()
        {
            return _precedence;
        }

        public override Expression Parse(Parser parser, Expression left, Token token)
        {
            parser.Consume();

            var right = parser.ParseExpression(_precedence - (_isRightAssociative ? 1 : 0));

            return new BinaryOperatorExpression { Left = left, Operator = token.Type, Right = right };
        }
    }
}
