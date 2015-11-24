using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate
{
    /// <summary>
    /// Interface for sytactic tree translation 
    /// </summary>
    public sealed class Translator : ITranslator
    {
        #region implement ITranslator
        /// <summary>
        /// Translate syntax tree using rules
        /// </summary>
        /// <param name="node">Root node</param>
        /// <returns>Rewritten root node</returns>
        public ISyntacticNode Translate(ISyntacticNode node) =>
            node.Rewrite(Rules);

        /// <summary>
        /// Check correctness of translation result.
        /// Throw exception if result isn't term tree.
        /// </summary>
        /// <param name="node">Root node of result</param>
        public void CheckResult(ISyntacticNode node) =>
            node.Rewrite(new SyntaxRewriter(
                nd => !(nd is TermSyntacticNode),
                (nd, ch) => { throw new TranslateException(); }));

        #endregion

        /// <summary>
        /// Translation rule list (rewriters)
        /// </summary>
        public IEnumerable<ISyntaxRewriter> Rules { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Translator"/> class.
        /// </summary>
        /// <param name="rules">Translation rule list</param>
        public Translator(IEnumerable<ISyntaxRewriter> rules)
        {
            Rules = rules;
        }
    }
}
