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

        // Key words
        Var,
        PrinLn,
        Loop,
        Func,
        Global,
        Return,

        // Literals
        IntegerLiteral,
        StringLiteral,

        // Binary operators
        Plus,
        Minus,
        Asterisk,
        Slash,

        Identifier,
        Assignment,
        Left_Paren,
        Right_Paren,
        Left_Curly_Bracket,
        Right_Curly_Bracket,
        Comma,
        Colon,
    }
}
