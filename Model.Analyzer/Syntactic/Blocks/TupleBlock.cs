using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks
{
    /// <summary>
    /// Syntactic block of function argument tupples
    /// </summary>
    public sealed class TupleBlock : Block
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="TupleBlock"/> class.
        /// </summary>
        /// <param name="name">Unique string rule indentifier</param>
        /// <param name="rule">Symblol sequence of context-free grammar</param>
        public TupleBlock(string name, IEnumerable<ISyntacticNodeType> rule) : base(name, rule)
        {
        }
    }
}
