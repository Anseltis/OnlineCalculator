using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers
{
    /// <summary>
    /// Class for resolve function term with unfixed argument's count
    /// </summary>
    public sealed class ParamFunctionResolver : IResolver
    {
        #region implement IResolver
        public IResolvedTerm Resolve(IDeclarationTerm term, IEnumerable<ISyntacticNode> children, ILinkedLibrary linkedLibrary)
        {
            var method = linkedLibrary.FindParamMethod(term.Identifier, children.Count());
            return method == null ? null : new ParamFunctionTerm(method, method.GetParameters().Length - 1);
        }
        #endregion
    }
}
