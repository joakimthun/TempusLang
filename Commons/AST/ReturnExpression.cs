﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.AST
{
    public class ReturnExpression : Expression
    {
        public Expression Expression { get; set; }
    }
}
