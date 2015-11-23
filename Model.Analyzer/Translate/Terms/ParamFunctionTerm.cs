using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for fixed argument count function term
    /// </summary>
    public class ParamFunctionTerm : IResolvedTerm
    {
        #region implement IResolvedTerm
        public Expression CreateExpression(Expression[] children)
        {
            var head = children.Take(ArgumentCount);
            var tail = children.Skip(ArgumentCount);
            var tailExpression = Expression.NewArrayInit(typeof(double), tail);
            var args = head.Concat(Enumerable.Repeat(tailExpression, 1));

            return Expression.Call(MethodInfo, args);
        }
        #endregion

        /// <summary>
        /// Function metadata
        /// </summary>
        public MethodInfo MethodInfo { get; }

        /// <summary>
        /// Argument count except param argument
        /// </summary>
        public int ArgumentCount {get;}

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamFunctionTerm"/> class.
        /// </summary>
        /// <param name="methodInfo">Function metadata</param>
        /// <param name="argumentCount">Argument count except param argument</param>
        public ParamFunctionTerm(MethodInfo methodInfo, int argumentCount)
        {
            MethodInfo = methodInfo;
            ArgumentCount = argumentCount;
        }

    }
}
