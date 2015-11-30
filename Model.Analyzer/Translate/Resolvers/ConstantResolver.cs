using System.Collections.Generic;
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
        public IResolvedTerm Resolve(IDeclarationTerm term, IEnumerable<ISyntacticNode> children, ILinkedLibrary linkedLibrary) => 
            linkedLibrary.FindConstant((IConstantDeclarationTerm)term);

        #endregion
    }
}
