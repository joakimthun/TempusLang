using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Commons.Lexing
{
    public class LanguageToken
    {
        public TokenType Type { get; set; }
        public Regex Regex { get; set; }
    }
}
