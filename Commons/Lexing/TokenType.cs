using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Lexing
{
    public enum TokenType
    {
        None,
        EOF,
        Identifier,
        Assignment,
        Plus,
        Minus,
        Asterisk,
        Slash,
        Var,
        PrinLn,
        Loop,
        Left_Paren,
        Right_Paren,
        Left_Bracket,
        Right_Bracket,
        Func,
        Comma,
        Colon,
        Return,
        IntegerLiteral,
        StringLiteral,
        Global,
    }
}
