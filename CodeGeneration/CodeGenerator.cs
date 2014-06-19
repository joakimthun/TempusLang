using Commons.AST;
using Commons.Exceptions;
using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public abstract class CodeGenerator
    {
        protected OpCode GetOperatorInstruction(TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.Asterisk:
                    return OpCodes.Mul;
                case TokenType.Slash:
                    return OpCodes.Div;
                case TokenType.Plus:
                    return OpCodes.Add;
                case TokenType.Minus:
                    return OpCodes.Sub;
                case TokenType.EqualityOp:
                    return OpCodes.Ceq;
                default:
                    throw new CodeGenerationException(string.Format("Unknown operator: {0}", tokenType.ToString()));
            }
        }
    }
}
