using System.Linq.Expressions;
using System.Reflection;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for fixed argument count function term
    /// </summary>
    public sealed class FunctionTerm : IResolvedTerm
    {
        #region implement IResolvedTerm
        public Expression CreateExpression(Expression[] children)
        {
            return Expression.Call(MethodInfo, children);
        }
        #endregion

        /// <summary>
        /// Function metadata
        /// </summary>
        public MethodInfo MethodInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionTerm"/> class.
        /// </summary>
        /// <param name="methodInfo">Function metadata</param>
        public FunctionTerm(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }


    }
}
