﻿using System.Collections.Generic;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for number term
    /// </summary>
    public sealed class NumberTerm : IResolvedTerm
    {
        #region implement IResolvedTerm
        public Expression CreateExpression(Expression[] children)
        {
            return Expression.Constant(Number);
        }
        #endregion

        /// <summary>
        /// Number value
        /// </summary>
        public double Number { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberTerm"/> class.
        /// </summary>
        /// <param name="number">Number value</param>
        public NumberTerm(double number)
        {
            Number = number;
        }
    }
}
