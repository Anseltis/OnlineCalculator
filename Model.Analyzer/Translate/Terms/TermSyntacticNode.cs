using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for syntactic tree's node with term
    /// </summary>
    public class TermSyntacticNode : ISyntacticNode
    {
        #region implement ISyntacticNode
        public IEnumerable<ISyntacticNode> Nodes { get; }
        public ISyntacticNode Rewrite(IEnumerable<ISyntacticNode> nodes) => 
            new TermSyntacticNode(Term, nodes);
        #endregion

        /// <summary>
        /// Term of current node
        /// </summary>
        public ITerm Term { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TermSyntacticNode"/> class.
        /// </summary>
        /// <param name="term">Term of current node</param>
        /// <param name="nodes">Children nodes</param>
        public TermSyntacticNode(ITerm term, IEnumerable<ISyntacticNode> nodes)
        {
            Term = term;
            Nodes = nodes;
        }
    }
}
