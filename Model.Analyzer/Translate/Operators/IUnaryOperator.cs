using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Interface for unary operator
    /// </summary>
    public interface IUnaryOperator : IOperator
    {
        /// <summary>
        /// Create expression for this operator.
        /// </summary>
        /// <param name="operand">Operand expression</param>
        /// <returns>Result expression</returns>
        Expression CreateExpression(Expression operand);
    }
}
