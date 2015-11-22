using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for lexical parsing of string expression
    /// </summary>
    public interface ILexicalAnalyzer
    {
        /// <summary>
        /// Parse input expression sting into token list.
        /// </summary>
        /// <param name="text">Input expression string</param>
        /// <returns>List of tokens</returns>
        IEnumerable<IToken> Parse(string text);
    }
}