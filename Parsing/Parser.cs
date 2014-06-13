using Commons.AST;
using Commons.Exceptions;
using Commons.Lexing;
using Parsing.Parselets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    public abstract class Parser
    {
        private List<Token> _tokens;
        private readonly Dictionary<TokenType, PrefixParselet> _prefixParselets;
        private readonly Dictionary<TokenType, InfixParselet> _infixParselets;
        private readonly Dictionary<TokenType, StatementParselet> _ll1StatementParselets;
        private readonly Dictionary<Tuple<TokenType, TokenType>, StatementParselet> _ll2StatementParselets;

        private int _index;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _index = -1;
            Consume();

            _prefixParselets = new Dictionary<TokenType, PrefixParselet>();
            _infixParselets = new Dictionary<TokenType, InfixParselet>();
            _ll1StatementParselets = new Dictionary<TokenType, StatementParselet>();
            _ll2StatementParselets = new Dictionary<Tuple<TokenType, TokenType>, StatementParselet>();
        }

        internal Token Lookahead { get; set; }

        public IEnumerable<Expression> ParseStatements(TokenType endOfStatementType = TokenType.None)
        {
            var statements = new List<Expression>();

            statements.Add(ParseStatement());

            while (true)
            {
                if (Match(endOfStatementType) || Match(TokenType.EOF))
                {
                    break;
                }

                statements.Add(ParseStatement());
            }

            return statements;
        }

        internal bool Match(TokenType tokenType)
        {
            return Lookahead.Type == tokenType;
        }

        internal void Consume(TokenType tokenType)
        {
            if (Lookahead.Type != tokenType)
            {
                throw new ParsingException(string.Format("Expected: {0} Found: {1}", tokenType, Lookahead.Type));
            }

            Consume();
        }

        internal void Consume()
        {
            _index++;

            if (_index < _tokens.Count)
            {
                Lookahead = _tokens[_index];
                return;
            }
        }

        internal Token Peek(int distance)
        {
            var targetIndex = _index + distance;

            if (targetIndex < _tokens.Count)
            {
                return _tokens[targetIndex];
            }

            return new Token { Type = TokenType.EOF };
        }

        internal Expression ParseStatement()
        {
            StatementParselet statementParselet;

            statementParselet = TryGetLl2Parselet(Lookahead.Type, Peek(1).Type);

            if(statementParselet == null)
                _ll1StatementParselets.TryGetValue(Lookahead.Type, out statementParselet);

            if (statementParselet != null)
                return statementParselet.Parse(this);

            return ParseExpression();
        }

        internal Expression ParseExpression()
        {
            return ParseExpression(0);
        }

        internal Expression ParseExpression(int precedence)
        {
            PrefixParselet prefixPareslet;
            _prefixParselets.TryGetValue(Lookahead.Type, out prefixPareslet);

            if (prefixPareslet == null)
                throw new ParsingException(string.Format("Unexpected token: {0}", Lookahead.Type));

            var left = prefixPareslet.Parse(this, Lookahead);

            while (precedence < GetPrecedence())
            {
                InfixParselet infixParselet;
                _infixParselets.TryGetValue(Lookahead.Type, out infixParselet);
                left = infixParselet.Parse(this, left, Lookahead);
            }

            return left;
        }

        protected void RegisterParselet(TokenType tokenType, PrefixParselet parselet)
        {
            _prefixParselets.Add(tokenType, parselet);
        }

        protected void RegisterParselet(TokenType tokenType, InfixParselet parselet)
        {
            _infixParselets.Add(tokenType, parselet);
        }

        protected void RegisterLl1Parselet(TokenType tokenType, StatementParselet parselet)
        {
            _ll1StatementParselets.Add(tokenType, parselet);
        }

        protected void RegisterLl2Parselet(TokenType tokenType1, TokenType tokenType2, StatementParselet parselet)
        {
            _ll2StatementParselets.Add(new Tuple<TokenType, TokenType>(tokenType1, tokenType2), parselet);
        }

        private StatementParselet TryGetLl2Parselet(TokenType tokenType1, TokenType tokenType2)
        {
            var key = new Tuple<TokenType, TokenType>(tokenType1, tokenType2);
            StatementParselet statementParselet;
            _ll2StatementParselets.TryGetValue(key, out statementParselet);

            return statementParselet;
        }

        private int GetPrecedence()
        {
            InfixParselet parselet;
            _infixParselets.TryGetValue(Lookahead.Type, out parselet);

            if (parselet == null)
                return 0;

            return parselet.GetPrecedence();
        }
    }
}
