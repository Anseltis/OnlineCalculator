using System;
using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Syntactic
{
    /// <summary>
    /// Class for syntactic parsing of token sequence
    /// </summary>
    public sealed class SyntacticAnalyzer : ISyntacticAnalyzer
    {
        #region implement ISyntacticAnalyzer
        /// <summary>
        /// Syntactic parse token sequence.
        /// </summary>
        /// <param name="target">Target non-terminal symbol of parsing</param>
        /// <param name="tokens">Input token sequence</param>
        /// <returns>Root node of syntactic tree</returns>
        /// <exception cref="SyntacticParseException">Expression doesn't pass syntactic analyzer</exception>
        public ISyntacticNode Parse(IEnumerable<IToken> tokens, ISyntacticNodeType target)
        {
            var tokenNodes = tokens
                .Select(token => new TokenSyntacticNode(token))
                .ToList();
            var node = target.Parse(tokenNodes, Rules)
                .Where(result => !result.TokenNodes.Any())
                .Select(result => result.Node)
                .FirstOrDefault();

            if (node == null)
            {
                var expression = string.Join("", tokens.Select(token => token.ToString()));
                throw new SyntacticParseException(expression);
            }
            return node;
        }
        #endregion

        /// <summary>
        /// Rules of context-free grammar
        /// </summary>
        public IEnumerable<IBlock> Rules { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyntacticAnalyzer"/> class.
        /// </summary>
        /// <param name="rules">Rules of context-free grammar</param>
        /// <exception cref="ArgumentNullException">Throws if rules are null</exception>
        public SyntacticAnalyzer(IEnumerable<IBlock> rules)
        {
            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }
            Rules = rules;
        }
    }
}
