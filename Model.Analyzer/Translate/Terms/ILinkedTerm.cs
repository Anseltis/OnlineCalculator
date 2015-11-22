using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Interface for term with real linking
    /// </summary>
    public interface ILinkedTerm : ITerm
    {
        /// <summary>
        /// Create expression for this term.
        /// </summary>
        /// <param name="children">Children's expression</param>
        /// <returns>Expression of the term</returns>
        Expression CreateExpression(Expression[] children);
    }
}
