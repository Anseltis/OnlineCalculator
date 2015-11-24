using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for create expression tree from translated linked tree
    /// </summary>
    public interface ICompiler
    {
        /// <summary>
        /// Compile translated tree and create expression tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        Expression<Func<double>> CreateExpression(ISyntacticNode node);
    }
}
