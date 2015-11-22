using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Class for multiplication operator
    /// </summary>
    public sealed class MultiplicationOperator : IBinaryOperator
    {
        #region implement IBinaryOperator
        public Expression CreateExpression(Expression left, Expression right) => 
            Expression.Multiply(left, right);
        #endregion
    }
}
