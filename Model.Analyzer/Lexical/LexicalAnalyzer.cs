﻿using System;
using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Lexical
{
    /// <summary>
    /// Class for lexical parsing of string expression
    /// </summary>
    public sealed class LexicalAnalyzer : ILexicalAnalyzer
    {
        #region implement ILexicalAnalyzer
        /// <summary>
        /// Compilated lexical rules
        /// </summary>
        public IEnumerable<CompiledLexicalRule> Rules { get; }

        /// <summary>
        /// Parse input expression sting into token list.
        /// </summary>
        /// <param name="text">Input expression string</param>
        /// <returns>List of tokens</returns>
        /// <exception cref="WrongLexicalRuleException">Thrown when wrong regular expression of rules</exception>
        /// <exception cref="LexicalParsingException">Thrown when input text isn't valid expression sting</exception>
        public IEnumerable<IToken> Parse(string text)
        {
            if (text.Trim() == "")
            {
                return Enumerable.Empty<IToken>();
            }

            var result = new List<IToken>();
            var current = text;
            while (current != "")
            {
                var rule = Rules.FirstOrDefault(r => r.IsMatch(current));

                if (rule == null)
                {
                    throw new LexicalParsingException(current);
                }

                if (rule.IsWrongRule(current))
                {
                    throw new WrongLexicalRuleException(rule.LexicalRule.Pattern);
                }

                result.Add(rule.CreateToken(current));
                current = rule.Tail(current);
            }

            return result.AsReadOnly();
        }
        #endregion

        /// <summary>
        ///  Initializes a new instance of the <see cref="LexicalAnalyzer"/> class.
        /// </summary>
        /// <param name="rules">Lexical rules</param>
        /// <exception cref="ArgumentNullException">Throw if rules are null</exception>
        public LexicalAnalyzer(IEnumerable<ILexicalRule> rules)
        {
            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }
            Rules = rules
                .Select(rule => new CompiledLexicalRule(rule))
                .ToList();
        }

    }
}
