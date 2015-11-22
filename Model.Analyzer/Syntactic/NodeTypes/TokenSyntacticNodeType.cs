using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.ParseResult;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes
{
    /// <summary>
    /// Class for terminal symbol of context-free grammar
    /// </summary>
    /// <typeparam name="T">Lexical token</typeparam>
    public sealed class TokenSyntacticNodeType<T> : ISyntacticNodeType where T : IToken
    {
        #region implement ISyntacticNodeType

        public IEnumerable<ISyntacticParseResult> Parse(IEnumerable<ITokenSyntacticNode> tokenNodes,
            IEnumerable<IBlock> rules)
        {
            var tokenNode = tokenNodes.FirstOrDefault();
            if (tokenNode?.Token is T)
            {
                yield return new SyntacticParseResult(tokenNodes.First(), tokenNodes.Skip(1));
            }

        }

        #endregion
    }
}
