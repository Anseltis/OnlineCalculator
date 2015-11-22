using System.Linq.Expressions;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Operators
{
    /// <summary>
    /// Class for binary plus operator
    /// </summary>
    public sealed class PlusOperator : IBinaryOperator
    {
        #region implement IBinaryOperator
        public Expression CreateExpression(Expression left, Expression right) => 
            Expression.Add(left, right);
        #endregion
    }
}
