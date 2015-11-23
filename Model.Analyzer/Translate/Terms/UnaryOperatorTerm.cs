using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for unary operation term
    /// </summary>
    public sealed class UnaryOperatorTerm : IResolvedTerm
    {
        #region implement IResolvedTerm
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
