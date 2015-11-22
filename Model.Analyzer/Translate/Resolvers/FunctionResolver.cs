using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers
{
    /// <summary>
    /// Class for resolve function term with fixed argument's count
    /// </summary>
    public sealed class FunctionResolver : IResolver
    {
        #region implement IResolver
        public ILinkedTerm Resolve(IDeclarationTerm term, IEnumerable<ISyntacticNode> children, ILinkedLibrary linkedLibrary)
        {
            var method = linkedLibrary.FindMethod(term.Identifier, children.Count());
            return method == null ? null : new FunctionTerm(method);
        }
        #endregion
    }
}
