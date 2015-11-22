using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Class for binary minus operator
    /// </summary>
    public sealed class MinusOperator : IBinaryOperator
    {
        #region implement IBinaryOperator
        public Expression CreateExpression(Expression left, Expression right) => 
            Expression.Subtract(left, right);
        #endregion
    }
}
