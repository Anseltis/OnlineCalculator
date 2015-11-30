using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Reflection
{
    /// <summary>
    /// Class for implement IResolvedTerm
    /// </summary>
    public sealed class ResolvedTerm : IResolvedTerm
    {
        #region implement IResolvedTerm
        public Expression CreateExpression(Expression[] children) => 
            ExpressionProducer(children);
        #endregion

        /// <summary>
        /// Producer for create expression
        /// </summary>
        private Func<Expression[], Expression> ExpressionProducer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvedTerm"/> class.
        /// </summary>
        /// <param name="expressionProducer">Producer for create expression</param>
        public ResolvedTerm(Func<Expression[], Expression> expressionProducer)
        {
            ExpressionProducer = expressionProducer;
        }
    }
}
