using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for syntactic tree translation
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Translate syntax tree using rules
        /// </summary>
        /// <param name="node">Root node</param>
        /// <returns>Rewritten root node</returns>
        ISyntacticNode Translate(ISyntacticNode node);

        /// <summary>
        /// Check correctness of translation result.
        /// Throw exception if result isn't term tree.
        /// </summary>
        /// <param name="node">Root node of result</param>
        void CheckResult(ISyntacticNode node);
    }
}