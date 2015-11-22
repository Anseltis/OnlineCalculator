using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Class for division operator
    /// </summary>
    public sealed class DivisionOperator : IBinaryOperator
    {
        #region implement IBinaryOperator
        public Expression CreateExpression(Expression left, Expression right) => 
            Expression.Divide(left, right);
        #endregion
    }
}
