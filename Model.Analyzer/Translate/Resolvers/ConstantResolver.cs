using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers
{
    /// <summary>
    /// Class for resolve constant term in syntactic tree
    /// </summary>
    public sealed class ConstantResolver : IResolver
    {
        #region implement IResolver
        public IResolvedTerm Resolve(IDeclarationTerm term, IEnumerable<ISyntacticNode> children, ILinkedLibrary linkedLibrary)
        {
            var propertyInfo = linkedLibrary.FindProperty(term.Identifier);
            return propertyInfo == null ? null : new ConstantTerm(propertyInfo);
        }
        #endregion
    }
}
