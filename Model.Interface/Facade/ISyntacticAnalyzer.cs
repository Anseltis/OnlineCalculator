using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for syntactic parsing of token sequence
    /// </summary>
    public interface ISyntacticAnalyzer
    {
        /// <summary>
        /// Syntactic parse token sequence.
        /// </summary>
        /// <param name="target">Target non-terminal symbol of parsing</param>
        /// <param name="tokens">Input token sequence</param>
        /// <returns>Root node of syntactic tree</returns>
        ISyntacticNode Parse(IEnumerable<IToken> tokens, ISyntacticNodeType target);
    }
}
