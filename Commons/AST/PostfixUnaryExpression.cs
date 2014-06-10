﻿using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class PostfixUnaryExpression : Expression
    {
        public Expression Left { get; set; }
        public TokenType Operator { get; set; }
    }
}
