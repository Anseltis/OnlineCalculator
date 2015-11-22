using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Class for unary minus operator
    /// </summary>
    public sealed class UnaryMinusOperator : IUnaryOperator
    {
        #region implement IUnaryOperator
        public Expression CreateExpression(Expression operand) => 
            Expression.Negate(operand);
        #endregion
    }
}
