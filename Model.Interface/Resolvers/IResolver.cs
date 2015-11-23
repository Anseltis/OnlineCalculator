using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Interface.Resolvers
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
        IResolvedTerm Resolve(IDeclarationTerm term, IEnumerable<ISyntacticNode> children, ILinkedLibrary linkedLibrary);
    }
}
