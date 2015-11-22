using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for resolving constant and function
    /// </summary>
    public interface ILinker
    {
        /// <summary>
        /// Resolve constant and function in translated sysntactic tree
        /// </summary>
        /// <param name="node">Root of translated tree</param>
        /// <returns>Resolving tree</returns>
        ISyntacticNode Resolve(ISyntacticNode node);

        /// <summary>
        /// Check correctness of resolve identifier.
        /// Throw exception if result isn't term tree.
        /// </summary>
        /// <param name="node">Root node of result</param>
        void CheckResult(ISyntacticNode node);
    }
}