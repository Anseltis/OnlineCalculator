using System;
using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers
{
    /// <summary>
    /// Interface for resolve declaration term in syntactic tree
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Resolve term from external class.
        /// </summary>
        /// <param name="term">Unresolved term</param>
        /// <param name="children">Rosolved children of the term</param>
        /// <param name="linkedLibrary">Resolving class</param>
        /// <returns>Resolver term</returns>
        ILinkedTerm Resolve(IDeclarationTerm term, IEnumerable<ISyntacticNode> children, ILinkedLibrary linkedLibrary);
    }
}
