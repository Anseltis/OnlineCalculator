﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter
{
    /// <summary>
    /// Static class of extention methods for syntactic tree
    /// </summary>
    public static class SyntacticNodeHelper
    {
        /// <summary>
        /// Translate syntactic tree using the rewriter.
        /// </summary>
        /// <param name="node">Root node</param>
        /// <param name="rewriter">Rewriter of rule</param>
        /// <returns>Rewritten root node</returns>
        public static ISyntacticNode Rewrite(this ISyntacticNode node, ISyntaxRewriter rewriter)
        {
            var children = node.Nodes.Select(nd => Rewrite(nd, rewriter));
            return rewriter.Filter(node)
                ? rewriter.Visit(node, children)
                : node.Rewrite(children);
        }

        /// <summary>
        /// Translate syntactic tree using the rewriters.
        /// </summary>
        /// <param name="node">Root node</param>
        /// <param name="rewriters">Rewriter list of rule</param>
        /// <returns>Rewritten root node</returns>
        public static ISyntacticNode Rewrite(this ISyntacticNode node, IEnumerable<ISyntaxRewriter> rewriters)
        {
            return rewriters
                .Aggregate(node, (current, rewriter) => current.Rewrite(rewriter));
        }

        /// <summary>
        /// Return true if this syntactic tree's node is block with target name.
        /// </summary>
        /// <param name="node">Current syntactic tree's node</param>
        /// <param name="name">target name of block</param>
        /// <returns>True if this syntactic tree's node is block with target name</returns>
        public static bool IsBlockOf(this ISyntacticNode node, string name)
        {
            var blockNode = node as BlockSyntacticNode;
            return blockNode?.Block?.Name == name;
        }

        /// <summary>
        /// Return true if this syntactic tree's node is block with target type.
        /// </summary>
        /// <typeparam name="T">Block type</typeparam>
        /// <param name="node">Current syntactic tree's node</param>
        /// <returns>True if this syntactic tree's node is block with target type</returns>
        public static bool IsBlockOf<T>(this ISyntacticNode node) where T : IBlock
        {
            var blockNode = node as BlockSyntacticNode;
            return blockNode?.Block is T;
        }

        public static ISyntacticNode ToLeftAssociationTree(this IEnumerable<ISyntacticNode> nodes,
            IDictionary<string, IBinaryOperator> operators)
        {
            return
                nodes.Skip(1).OfType<TermSyntacticNode>()
                    .Zip(nodes.OfType<TokenSyntacticNode>(),
                        ((termNode, tokenNode) => new {TermNode = termNode, TokenNode = tokenNode}))
                    .Aggregate(nodes.First(),
                        (termNode, rec) => new TermSyntacticNode(
                            new BinaryOperatorTerm(
                                operators[rec.TokenNode.Token.Lexeme]),
                            Enumerable.Repeat(termNode, 1).Concat(Enumerable.Repeat(rec.TermNode,1))));
        }

        public static Expression CreateExpression(this ISyntacticNode node)
        {
            var children = node.Nodes
                .Select(nd => nd.CreateExpression())
                .ToArray();
            var termNode = (TermSyntacticNode) node;
            var term = (ILinkedTerm) termNode.Term;
            return term.CreateExpression(children);
        }

    }
}
 