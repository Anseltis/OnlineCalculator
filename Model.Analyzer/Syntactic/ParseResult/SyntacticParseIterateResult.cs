using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Syntactic.ParseResult
{
    /// <summary>
    /// Class for storage iterate data of sysntact parse
    /// </summary>
    public sealed class SyntacticParseIterateResult : ISyntacticParseIterateResult
    {
        #region implement ISyntacticParseIterateResult
        /// <summary>
        /// Result nodes for read element of syntactic rules 
        /// </summary>
        public IEnumerable<ISyntacticNode> Nodes { get; }

        /// <summary>
        /// Non-read element of syntactic rules
        /// </summary>
        public IEnumerable<ISyntacticNodeType> NodeTypes { get; }

        /// <summary>
        /// Non-read token sequence
        /// </summary>
        public IEnumerable<ITokenSyntacticNode> TokenNodes { get; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SyntacticParseIterateResult"/> class.
        /// </summary>
        /// <param name="nodes">Result nodes for read element of syntactic rules</param>
        /// <param name="nodeTypes">Non-read element of syntactic rules</param>
        /// <param name="tokenNodes">Non-read token sequence</param>
        public SyntacticParseIterateResult(IEnumerable<ISyntacticNode> nodes,
            IEnumerable<ISyntacticNodeType> nodeTypes, IEnumerable<ITokenSyntacticNode> tokenNodes)
        {
            Nodes = nodes;
            NodeTypes = nodeTypes;
            TokenNodes = tokenNodes;
        }

    }
}
