using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for unary operation term
    /// </summary>
    public sealed class UnaryOperatorTerm : ILinkedTerm
    {
        #region implement ILinkedTerm
        public Expression CreateExpression(Expression[] children)
        {
            return Operator.CreateExpression(children[0]);
        }
        #endregion

        /// <summary>
        /// Unary operation
        /// </summary>
        public IUnaryOperator Operator { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryOperatorTerm"/> class.
        /// </summary>
        /// <param name="op"></param>
        public UnaryOperatorTerm(IUnaryOperator op)
        {
            Operator = op;
        }

    }
}
