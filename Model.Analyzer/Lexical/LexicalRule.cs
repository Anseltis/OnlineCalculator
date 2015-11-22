using System;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Lexical
{
    /// <summary>
    /// Class for rules for lexiacl analyzer
    /// </summary>
    public sealed class LexicalRule : ILexicalRule
    {
        #region implement ILexicalRule
        public string Pattern { get; }
        public Func<ITokenBuilder, IToken> TokenFactory { get; }
        #endregion

        /// <summary>
        ///  Initializes a new instance of the <see cref="LexicalRule"/> class.
        /// </summary>
        /// <param name="pattern">Regular expression for retrieve token</param>
        /// <param name="factory">Factory for create token</param>
        /// <exception cref="ArgumentNullException">Throw if pattern or factory is null</exception>
        public LexicalRule(string pattern, Func<ITokenBuilder, IToken> factory)
        {
            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            Pattern = pattern;
            TokenFactory = factory;
        }
    }
}
