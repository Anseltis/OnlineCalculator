using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Syntactic.ParseResult
{
    /// <summary>
    /// Class for storage data of sysntact parse
    /// </summary>
    public sealed class SyntacticParseResult : ISyntacticParseResult
    {
        #region implement ISyntacticParseResult
        /// <summary>
        /// Syntactic node
        /// </summary>
        public ISyntacticNode Node { get;  }

        /// <summary>
        /// Non-read token sequence
        /// </summary>
        public IEnumerable<ITokenSyntacticNode> TokenNodes { get; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SyntacticParseResult"/> class.
        /// </summary>
        /// <param name="node">Syntactic node</param>
        /// <param name="tokenNodes">Non-read token sequence</param>
        public SyntacticParseResult(ISyntacticNode node, IEnumerable<ITokenSyntacticNode> tokenNodes)
        {
            Node = node;
            TokenNodes = tokenNodes;
        }
    }


}
