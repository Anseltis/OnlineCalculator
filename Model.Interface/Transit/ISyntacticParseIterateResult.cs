using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Transit
{
    /// <summary>
    /// Interface for storage iterate  data of sysntact parse
    /// </summary>
    public interface ISyntacticParseIterateResult
    {
        /// <summary>
        /// Result nodes for read element of syntactic rules 
        /// </summary>
        IEnumerable<ISyntacticNode> Nodes { get; }

        /// <summary>
        /// Non-read element of syntactic rules
        /// </summary>
        IEnumerable<ISyntacticNodeType> NodeTypes { get; }

        /// <summary>
        /// Non-read token sequence
        /// </summary>
        IEnumerable<ITokenSyntacticNode> TokenNodes { get; }
    }
}