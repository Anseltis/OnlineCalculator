using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for constant term
    /// </summary>
    public sealed class ConstantTerm : ILinkedTerm
    {
        #region implement ILinkedTerm
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
