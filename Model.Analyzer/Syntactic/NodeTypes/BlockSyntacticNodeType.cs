using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.ParseResult;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes
{
    /// <summary>
    /// Class for non-terminal symbol of context-free grammar
    /// </summary>
    /// <typeparam name="T">Syntactic block</typeparam>
    public sealed class BlockSyntacticNodeType<T> : ISyntacticNodeType where T : IBlock
    {
        #region implement ISyntacticNodeType
        public IEnumerable<ISyntacticParseResult> Parse(IEnumerable<ITokenSyntacticNode> tokenNodes,
            IEnumerable<IBlock> rules)
        {
            return rules
                .OfType<T>()
                .Where(block => block.Rule.Any())
                .Select(
                    block =>
                        new
                        {
                            Block = block,
                            Iteration =
                                new SyntacticParseIterateResult(
                                    Enumerable.Empty<ISyntacticNode>(),
                                    block.Rule, tokenNodes)
                        })
                .SelectMany(
                    rec => rec.Iteration.Iterate(rules)
                        .Select(iteration => new { Block = rec.Block, Iteration = iteration }))
                .Select(
                    rec =>
                        new SyntacticParseResult(
                            new BlockSyntacticNode(rec.Block, rec.Iteration.Nodes),
                            rec.Iteration.TokenNodes));
        }
        #endregion
    }
}
