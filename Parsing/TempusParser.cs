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
            RegisterLl1Parselet(TokenType.Var, new VariableDeclarationParselet());
            RegisterLl1Parselet(TokenType.PrinLn, new PrintLineParselet());
            RegisterLl1Parselet(TokenType.Loop, new LoopParselet());
            RegisterLl1Parselet(TokenType.Func, new FuncParselet());
            RegisterLl1Parselet(TokenType.Return, new ReturnParselet());
            RegisterLl1Parselet(TokenType.Global, new FieldDeclarationParselet());
            RegisterLl1Parselet(TokenType.If, new ConditionalStatementParselet());

            RegisterLl2Parselet(TokenType.Identifier, TokenType.Left_Paren, new FuncInvocationParselet());

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
