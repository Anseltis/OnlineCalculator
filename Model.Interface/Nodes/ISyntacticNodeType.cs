using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Interface.Nodes
{
    /// <summary>
    /// Interface for elements of syntactic rules
    /// </summary>
    /// Implement Union Type Pattern
    public interface ISyntacticNodeType
    {
        /// <summary>
        /// Syntactic parse current element of syntactic rules
        /// </summary>
        /// <param name="tokenNodes">Token nodes tail</param>
        /// <param name="rules">Sysntactic analyzer rules</param>
        /// <returns></returns>
        IEnumerable<ISyntacticParseResult> Parse(
            IEnumerable<ITokenSyntacticNode> tokenNodes, IEnumerable<IBlock> rules);
    }
}
