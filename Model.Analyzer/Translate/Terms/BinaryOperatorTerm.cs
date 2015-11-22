using System.Collections.Generic;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for binary operator term
    /// </summary>
    public sealed class BinaryOperatorTerm : ILinkedTerm
    {
        #region implement ILinkedTerm
        public Expression CreateExpression(Expression[] children)
        {
            return Operator.CreateExpression(children[0], children[1]);
        }
        #endregion

        /// <summary>
        /// Binary operation
        /// </summary>
        public IBinaryOperator Operator { get; }

        /// <summary>
        /// /// Initializes a new instance of the <see cref="BinaryOperatorTerm"/> class.
        /// </summary>
        /// <param name="op">Binary operation</param>
        public BinaryOperatorTerm(IBinaryOperator op)
        {
            Operator = op;
        }
    }
}
