using Commons.Lexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lexing
{
    public class Lexer
    {
        private string _input;
        private List<Token> _matchingTokens;
        private IReadOnlyCollection<LanguageToken> _languageTokens;

        public Lexer(string input)
        {
            _input = input;
            _matchingTokens = new List<Token>();
            CreateLanguageTokens();
        }

        public List<Token> GetTokens()
        {
            Token token;
            do
            {
                token = GetNextMatchingToken();
                if (token.Type == TokenType.EOF)
                {
                    _matchingTokens.Add(new Token { Type = TokenType.EOF });
                    return _matchingTokens;
                }

                _matchingTokens.Add(token);
            }
            while (token.Type != TokenType.EOF);

            return _matchingTokens;
        }

        private Token GetNextMatchingToken()
        {
            var matchingTokens = new List<Token>();

            foreach (var languageToken in _languageTokens)
            {
                var match = languageToken.Regex.Match(_input);
                if (match.Success)
                {
                    matchingTokens.Add(new Token
                    {
                        Index = match.Index,
                        Type = languageToken.Type,
                        Value = match.Value,
                    });
                }
            }

            if (matchingTokens.Any())
            {
                var firstMatchingToken = matchingTokens.OrderBy(x => x.Index).First();
                var munch = firstMatchingToken.Index + firstMatchingToken.Value.Length;
                _input = _input.Substring(munch);

                if (firstMatchingToken.Type == TokenType.StringLiteral)
                    firstMatchingToken.Value = firstMatchingToken.Value.Substring(1, firstMatchingToken.Value.Length - 2);

                return firstMatchingToken;
            }

            return new Token { Type = TokenType.EOF };
        }

        private void CreateLanguageTokens()
        {
            _languageTokens = new List<LanguageToken> 
            {
                new LanguageToken
                {
                    Type = TokenType.Var,
                    Regex = new Regex("var"),
                },
                new LanguageToken
                {
                    Type = TokenType.PrinLn,
                    Regex = new Regex("println"),
                },
                new LanguageToken
                {
                    Type = TokenType.Loop,
                    Regex = new Regex("loop"),
                },
                new LanguageToken
                {
                    Type = TokenType.Func,
                    Regex = new Regex("func"),
                },
                new LanguageToken
                {
                    Type = TokenType.Global,
                    Regex = new Regex("global"),
                },
                new LanguageToken
                {
                    Type = TokenType.Return,
                    Regex = new Regex("return"),
                },
                new LanguageToken
                {
                    Type = TokenType.IntegerLiteral,
                    Regex = new Regex("[0-9]+"),
                },
                new LanguageToken
                {
                    Type = TokenType.StringLiteral,
                    //Regex = new Regex("'[a-zA-Z]+ ?([a-zA-Z]+)?'"),
                    Regex = new Regex("'[^']+'"),
                },
                new LanguageToken
                {
                    Type = TokenType.Plus,
                    Regex = new Regex(@"\+"),
                },
                new LanguageToken
                {
                    Type = TokenType.Minus,
                    Regex = new Regex(@"\-"),
                },
                new LanguageToken
                {
                    Type = TokenType.Asterisk,
                    Regex = new Regex(@"\*"),
                },
                new LanguageToken
                {
                    Type = TokenType.Slash,
                    Regex = new Regex("/"),
                },
                new LanguageToken
                {
                    Type = TokenType.Identifier,
                    Regex = new Regex("[a-z]+([a-zA-Z]+)?"),
                },
                new LanguageToken
                {
                    Type = TokenType.Assignment,
                    Regex = new Regex("="),
                },
                new LanguageToken
                {
                    Type = TokenType.Left_Paren,
                    Regex = new Regex(@"\("),
                },
                new LanguageToken
                {
                    Type = TokenType.Right_Paren,
                    Regex = new Regex(@"\)"),
                },
                new LanguageToken
                {
                    Type = TokenType.Left_Curly_Bracket,
                    Regex = new Regex(@"\{"),
                },
                new LanguageToken
                {
                    Type = TokenType.Right_Curly_Bracket,
                    Regex = new Regex(@"\}"),
                },
                new LanguageToken
                {
                    Type = TokenType.Comma,
                    Regex = new Regex(","),
                },
                new LanguageToken
                {
                    Type = TokenType.Colon,
                    Regex = new Regex(":"),
                },
            };
        }
    }
}
