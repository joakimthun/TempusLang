using CodeGeneration;
using Commons.Lexing;
using Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokens = new List<Token> 
            {
                new Token { Type = TokenType.Identifier, Value = "a" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.Identifier, Value = "b" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "c" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "d" },
                new Token { Type = TokenType.EOF },
            };

            var parser = new TempusParser(tokens);
            var ast = parser.ParseStatements();

            var tokens2 = new List<Token> 
            {
                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "a" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.Identifier, Value = "b" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "c" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "d" },
                new Token { Type = TokenType.EOF },
            };

            var parser2 = new TempusParser(tokens2);
            var ast2 = parser2.ParseStatements();

            var tokens3 = new List<Token> 
            {
                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "a" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.Identifier, Value = "b" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "c" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "d" },
                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "a" },
                new Token { Type = TokenType.EOF },
            };

            var parser3 = new TempusParser(tokens3);
            var ast3 = parser3.ParseStatements();

            var tokens4 = new List<Token> 
            {
                new Token { Type = TokenType.Loop, Value = "loop" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Identifier, Value = "5" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "10" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Bracket, Value = "{" },
                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "a" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.Identifier, Value = "b" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "c" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "d" },
                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "70" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "70" },
                new Token { Type = TokenType.Right_Bracket, Value = "}" },
                new Token { Type = TokenType.EOF },
            };

            var parser4 = new TempusParser(tokens4);
            var ast4 = parser4.ParseStatements();

            var tokens5 = new List<Token> 
            {
                new Token { Type = TokenType.Func, Value = "func" },
                new Token { Type = TokenType.Identifier, Value = "myFunc" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Identifier, Value = "arg1" },
                new Token { Type = TokenType.Comma, Value = "," },
                new Token { Type = TokenType.Identifier, Value = "arg2" },
                new Token { Type = TokenType.Comma, Value = "," },
                new Token { Type = TokenType.Identifier, Value = "arg3" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Bracket, Value = "{" },
                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "a" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.Identifier, Value = "b" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "c" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "d" },
                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "70" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "70" },
                new Token { Type = TokenType.Right_Bracket, Value = "}" },
                new Token { Type = TokenType.EOF },
            };

            var parser5 = new TempusParser(tokens5);
            var ast5 = parser5.ParseStatements();

            var tokens6 = new List<Token> 
            {
                new Token { Type = TokenType.Func, Value = "func" },
                new Token { Type = TokenType.Identifier, Value = "myFunc" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Identifier, Value = "arg1" },
                new Token { Type = TokenType.Comma, Value = "," },
                new Token { Type = TokenType.Identifier, Value = "arg2" },
                new Token { Type = TokenType.Comma, Value = "," },
                new Token { Type = TokenType.Identifier, Value = "arg3" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Bracket, Value = "{" },
                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "a" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.Identifier, Value = "b" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "c" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "d" },
                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "70" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.Identifier, Value = "70" },
                new Token { Type = TokenType.Return, Value = "return" },
                new Token { Type = TokenType.Identifier, Value = "arg1" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "arg2" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.Identifier, Value = "arg3" },
                new Token { Type = TokenType.Right_Bracket, Value = "}" },
                new Token { Type = TokenType.EOF },
            };

            var parser6 = new TempusParser(tokens6);
            var ast6 = parser6.ParseStatements();

            var program = new List<Token> 
            {
                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "myString" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.StringLiteral, Value = "Hello World!" },
                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "myString" },
                new Token { Type = TokenType.EOF },
            };

            var programParser = new TempusParser(program);
            var programAst = programParser.ParseStatements();
            var codeGenerator = new CodeGenerator(programAst, "Tempus1.exe");
        }
    }
}
