using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Transit
{
    /// <summary>
    /// Interface for storage data of sysntact parse
    /// </summary>
    public interface ISyntacticParseResult
    {
        /// <summary>
        /// Syntactic node
        /// </summary>
        ISyntacticNode Node { get; }

        /// <summary>
        /// Non-read token sequence
        /// </summary>
        IEnumerable<ITokenSyntacticNode> TokenNodes { get; }
    }
}