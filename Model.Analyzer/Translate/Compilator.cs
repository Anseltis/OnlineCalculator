using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate
{
    /// <summary>
    /// Class for create expression tree from translated linked tree
    /// </summary>
    public sealed class Compilator : ICompilator
    {
        #region implement ICompile
        public Expression<Func<double>> CreateExpression(ISyntacticNode node) => 
            Expression.Lambda<Func<double>>(node.CreateExpression());
        #endregion
    }
}
