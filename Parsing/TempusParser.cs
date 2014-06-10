using Commons.Lexing;
using Parsing.Parselets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    public class TempusParser : Parser
    {
        public TempusParser(List<Token> tokens) : base(tokens)
        {
            RegisterParselet(TokenType.Var, new VariableDeclarationParselet());
            RegisterParselet(TokenType.PrinLn, new PrintLineParselet());
            RegisterParselet(TokenType.Loop, new LoopParselet());
            RegisterParselet(TokenType.Func, new FuncParselet());
            RegisterParselet(TokenType.Return, new ReturnParselet());

            RegisterParselet(TokenType.Identifier, new IdentifierParselet());
            RegisterParselet(TokenType.IntegerLiteral, new IntegerLiteralParselet());
            RegisterParselet(TokenType.StringLiteral, new StringLiteralParselet());

            RegisterParselet(TokenType.Assignment, new AssignmentParselet());

            RegisterLeftAssociativeInfixParselet(TokenType.Plus, Precedence.Sum);
            RegisterLeftAssociativeInfixParselet(TokenType.Minus, Precedence.Sum);
            RegisterLeftAssociativeInfixParselet(TokenType.Asterisk, Precedence.Product);
            RegisterLeftAssociativeInfixParselet(TokenType.Slash, Precedence.Product);
        }

        private void RegisterLeftAssociativeInfixParselet(TokenType tokenType, int precedence)
        {
            RegisterParselet(tokenType, new BinaryOperatorParselet(precedence, false));
        }

        private void RegisterRightAssociativeInfixParselet(TokenType tokenType, int precedence)
        {
            RegisterParselet(tokenType, new BinaryOperatorParselet(precedence, true));
        }
    }
}
