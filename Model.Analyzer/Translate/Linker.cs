using System;
using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Terms;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate
{
    /// <summary>
    /// Class for resolving constant and function
    /// </summary>
    public sealed class Linker : ILinker
    {
        #region implement ILinker
        /// <summary>
        /// Resolve constant and function in translated sysntactic tree
        /// </summary>
        /// <param name="node">Root of translated tree</param>
        /// <returns>Resolving tree</returns>
        public ISyntacticNode Resolve(ISyntacticNode node) => node.Rewrite(Rules);

        /// <summary>
        /// Check correctness of resolve identifier.
        /// Throw exception if result isn't term tree.
        /// </summary>
        /// <param name="node">Root node of result</param>
        /// <exception cref="CannotResolveIdentifierException">Error if trnslation tree contain non-term nodes</exception>
        public void CheckResult(ISyntacticNode node) =>
            node.Rewrite(new SyntaxRewriter(
                nd => !((node as TermSyntacticNode)?.Term is IResolvedTerm),
                (nd, ch) => { throw new TranslateException(); }));
        #endregion

        /// <summary>
        /// Rewriter rules for resolving constant and functions
        /// </summary>
        public IEnumerable<ISyntaxRewriter> Rules { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Linker"/> class.
        /// </summary>
        /// <param name="rules">Resolving rule dictionary</param>
        /// <param name="linkedLibrary">Resolving class</param>
        public Linker(IDictionary<IResolverType,IEnumerable<IResolver>> rules, ILinkedLibrary linkedLibrary)
        {
            if (linkedLibrary == null)
            {
                throw new ArgumentNullException(nameof(linkedLibrary));
            }

            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }

            Rules = rules
                .Select(pair => new ResolverSyntaxRewriter(pair.Key, pair.Value, linkedLibrary));
        }

    }
}
