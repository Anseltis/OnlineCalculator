using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Interface for binary operator
    /// </summary>
    public interface IBinaryOperator : IOperator
    {
        /// <summary>
        /// Create expression for this operator.
        /// </summary>
        /// <param name="left">Left operand expression</param>
        /// <param name="right">Rignt operand expression</param>
        /// <returns>Result expression</returns>
        Expression CreateExpression(Expression left, Expression right);
    }
}
