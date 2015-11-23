using System;
using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Terms;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter
{
    /// <summary>
    ///  Class for modify syntactic tree for sake of the resolving unknown identifiers
    /// </summary>
    public class ResolverSyntaxRewriter : ISyntaxRewriter
    {
        #region implenet ISyntaxRewriter
        /// <summary>
        /// Filter node for visit.
        /// </summary>
        /// <param name="node">Current node</param>
        /// <returns>True if visitor must visit this node</returns>
        public bool Filter(ISyntacticNode node)
        {
            var termNode = node as TermSyntacticNode;
            return termNode != null && ResolverType.Resolve(termNode.Term);
        }

        /// <summary>
        /// Rewrite visited node.
        /// </summary>
        /// <param name="node">Current node</param>
        /// <param name="children">Rewritten chilren node</param>
        /// <returns>Rewritten node</returns>
        /// <exception cref="CannotResolveIdentifierException">Throw if there is unresolved identifier</exception>
        public ISyntacticNode Visit(ISyntacticNode node, IEnumerable<ISyntacticNode> children)
        {
            var termNode = (TermSyntacticNode) node;
            var term = (IDeclarationTerm) termNode.Term;
            var linkedTerm = Resolvers
                .Select(resolver => resolver.Resolve(term, children, LinkedLibrary))
                .FirstOrDefault(lt => lt != null);
            if (linkedTerm == null)
            {
                throw new CannotResolveIdentifierException(term.Identifier);
            }
            return new TermSyntacticNode(linkedTerm, children);
        }
        #endregion

        /// <summary>
        /// Declaration term type for resolving
        /// </summary>
        public IResolverType ResolverType { get; }

        /// <summary>
        /// List of resolvers
        /// </summary>
        public IEnumerable<IResolver> Resolvers { get; }

        /// <summary>
        /// Resolving class
        /// </summary>
        public ILinkedLibrary LinkedLibrary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolverSyntaxRewriter"/> class.
        /// </summary>
        /// <param name="resolverType">Declaration term type for resolving</param>
        /// <param name="resolvers">List of resolvers</param>
        /// <param name="linkedLibrary">Resolving class</param>
        public ResolverSyntaxRewriter(IResolverType resolverType, IEnumerable<IResolver> resolvers, ILinkedLibrary linkedLibrary)
        {
            ResolverType = resolverType;
            Resolvers = resolvers;
            LinkedLibrary = linkedLibrary;
        }

    }
}
