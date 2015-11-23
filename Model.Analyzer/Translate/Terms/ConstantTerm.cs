using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for constant term
    /// </summary>
    public sealed class ConstantTerm : IResolvedTerm
    {
        #region implement IResolvedTerm
        public Expression CreateExpression(Expression[] children)
        {
            return Expression.Property(null, PropertyInfo);
        }
        #endregion

        /// <summary>
        /// Constant metadata
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantTerm"/> class.
        /// </summary>
        /// <param name="propertyInfo">Constant metadata</param>
        public ConstantTerm(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

    }
}
