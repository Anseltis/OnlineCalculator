using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Class for unary plus operator
    /// </summary>
    public sealed class UnaryPlusOperator : IUnaryOperator
    {
        #region implement IUnaryOperator
        public Expression CreateExpression(Expression operand) => 
            Expression.UnaryPlus(operand);
        #endregion
    }
}
