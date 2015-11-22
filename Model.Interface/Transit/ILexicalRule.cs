using System;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Transit
{
    /// <summary>
    /// Interface for rules for lexiacl analyzer
    /// </summary>
    public interface ILexicalRule
    {
        /// <summary>
        /// Regular expression for retrieve token
        /// Expression contain four section: left trivia (l), lexeme (t), right trivia (r) and tail (e)
        /// </summary>
        string Pattern { get; }

        /// <summary>
        /// Factory for create token
        /// </summary>
        Func<ITokenBuilder, IToken> TokenFactory { get; }
    }
}