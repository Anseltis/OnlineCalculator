using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Syntactic.ParseResult
{
    /// <summary>
    /// 
    /// </summary>
    public static class SyntacticParseIterateResultHelper
    {
        /// <summary>
        /// Iterate of parsing for the rule.
        /// </summary>
        /// <param name="iteration">Current iteration</param>
        /// <param name="rules">Syntactic analyzer rules</param>
        /// <returns>Next iteration</returns>
        public static IEnumerable<ISyntacticParseIterateResult> Iterate(this ISyntacticParseIterateResult iteration,
            IEnumerable<IBlock> rules)
        {
            if (!iteration.NodeTypes.Any())
            {
                return Enumerable.Repeat(iteration, 1);
            }

            var nodeType = iteration.NodeTypes.First();
            return nodeType.Parse(iteration.TokenNodes, rules)
                .Select(res => new SyntacticParseIterateResult(
                    iteration.Nodes.Concat(Enumerable.Repeat(res.Node, 1)),
                    iteration.NodeTypes.Skip(1), res.TokenNodes))
                .SelectMany(it => it.Iterate(rules));
        }
    }
}
