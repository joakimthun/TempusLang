using CodeGeneration;
using Commons.AST;
using Commons.Lexing;
using Lexing;
using Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class TempusCompiler
    {
        const string SuperHardCodedFilePath = @"C:\Dev\Tempus\Compiler\Programs\Program{0}.tempus";

        static void Main(string[] args)
        {
            //OldTestStuff();
            CompileProgram(1);
            CompileProgram(2);
            CompileProgram(3);
            CompileProgram(4);
            
        }

        static void CompileProgram(int number)
        {
            string input = File.ReadAllText(string.Format(SuperHardCodedFilePath, number));

            var lexer = new Lexer(input);
            var tokens = lexer.GetTokens();

            var parser = new TempusParser(tokens);
            var program = parser.ParseStatements();

            var assemblyGenerator = new AssemblyGenerator(program, string.Format("Program{0}.exe", number));
        }

        private static void OldTestStuff()
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
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },
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
                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },
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
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },
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
                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },
                new Token { Type = TokenType.EOF },
            };

            var parser5 = new TempusParser(tokens5);
            var ast5 = parser5.ParseStatements();

            var tokens6 = new List<Token> 
            {
                new Token { Type = TokenType.Func, Value = "func" },
                new Token { Type = TokenType.Identifier, Value = "main" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Identifier, Value = "arg1" },
                new Token { Type = TokenType.Comma, Value = "," },
                new Token { Type = TokenType.Identifier, Value = "arg2" },
                new Token { Type = TokenType.Comma, Value = "," },
                new Token { Type = TokenType.Identifier, Value = "arg3" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },
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
                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },
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
            //var codeGenerator = new AssemblyGenerator(programAst, "Tempus1.exe");

            var program2 = new List<Token> 
            {
                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "myString" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.StringLiteral, Value = "Hello World!" },
                new Token { Type = TokenType.Loop, Value = "loop" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.IntegerLiteral, Value = "10" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },
                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "myString" },
                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },
                new Token { Type = TokenType.EOF },
            };

            var programParser2 = new TempusParser(program2);
            var programAst2 = programParser2.ParseStatements();
            //var codeGenerator2 = new AssemblyGenerator(programAst2, "Tempus2.exe");

            var program3 = new List<Token> 
            {
                new Token { Type = TokenType.Global, Value = "global" },
                new Token { Type = TokenType.Identifier, Value = "myField" },
                new Token { Type = TokenType.Colon, Value = ":" },
                new Token { Type = TokenType.Identifier, Value = "string" },
                new Token { Type = TokenType.Func, Value = "func" },
                new Token { Type = TokenType.Identifier, Value = "main" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },
                new Token { Type = TokenType.Identifier, Value = "myField" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.StringLiteral, Value = "Hello World From Field!" },
                new Token { Type = TokenType.Loop, Value = "loop" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.IntegerLiteral, Value = "10" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },
                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "myField" },
                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },
                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },
                new Token { Type = TokenType.EOF },
            };

            var programParser3 = new TempusParser(program3);
            var programAst3 = programParser3.ParseStatements();
            var codeGenerator3 = new AssemblyGenerator(programAst3, "Tempus1.exe");

            var program4 = new List<Token> 
            {
                new Token { Type = TokenType.Global, Value = "global" },
                new Token { Type = TokenType.Identifier, Value = "myField" },
                new Token { Type = TokenType.Colon, Value = ":" },
                new Token { Type = TokenType.Identifier, Value = "string" },
                
                new Token { Type = TokenType.Func, Value = "func" },
                new Token { Type = TokenType.Identifier, Value = "printField" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },

                new Token { Type = TokenType.Var, Value = "var" },
                new Token { Type = TokenType.Identifier, Value = "result" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.IntegerLiteral, Value = "10" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.IntegerLiteral, Value = "5" },
                new Token { Type = TokenType.Plus, Value = "+" },
                new Token { Type = TokenType.IntegerLiteral, Value = "5" },
                new Token { Type = TokenType.Slash, Value = "/" },
                new Token { Type = TokenType.IntegerLiteral, Value = "6" },
                new Token { Type = TokenType.Asterisk, Value = "*" },
                new Token { Type = TokenType.IntegerLiteral, Value = "66" },

                //new Token { Type = TokenType.Loop, Value = "loop" },
                //new Token { Type = TokenType.Left_Paren, Value = "(" },
                //new Token { Type = TokenType.IntegerLiteral, Value = "10" },
                //new Token { Type = TokenType.Right_Paren, Value = ")" },
                //new Token { Type = TokenType.Left_Bracket, Value = "{" },
                //
                //new Token { Type = TokenType.PrinLn, Value = "println" },
                //new Token { Type = TokenType.Identifier, Value = "myField" },
                //
                //new Token { Type = TokenType.PrinLn, Value = "println" },
                //new Token { Type = TokenType.Identifier, Value = "index" },
                //
                //new Token { Type = TokenType.Right_Bracket, Value = "}" },

                new Token { Type = TokenType.PrinLn, Value = "println" },
                new Token { Type = TokenType.Identifier, Value = "result" },

                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },

                new Token { Type = TokenType.Func, Value = "func" },
                new Token { Type = TokenType.Identifier, Value = "main" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Left_Curly_Bracket, Value = "{" },
                new Token { Type = TokenType.Identifier, Value = "myField" },
                new Token { Type = TokenType.Assignment, Value = "=" },
                new Token { Type = TokenType.StringLiteral, Value = "Hello World From Field!" },

                new Token { Type = TokenType.Identifier, Value = "printField" },
                new Token { Type = TokenType.Left_Paren, Value = "(" },
                new Token { Type = TokenType.Right_Paren, Value = ")" },
                new Token { Type = TokenType.Right_Curly_Bracket, Value = "}" },

                new Token { Type = TokenType.EOF },
            };

            var programParser4 = new TempusParser(program4);
            var programAst4 = programParser4.ParseStatements();
            var codeGenerator4 = new AssemblyGenerator(programAst4, "Tempus2.exe");
        }
    }
}
