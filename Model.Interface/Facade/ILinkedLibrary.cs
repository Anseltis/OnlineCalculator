using System.Reflection;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for external source of resolving constants and functions
    /// </summary>
    /// TODO: Implement visitor pattern 
    public interface ILinkedLibrary
    {
        /// <summary>
        /// Find method with target name, double parameters, double return type and last param adouble argument.
        /// </summary>
        /// <param name="term">Traget term</param>
        /// <returns>MethodInfo of found class or null</returns>
        IResolvedTerm FindParamFunction(IFunctionDeclarationTerm term);

        /// <summary>
        /// Find method with target name, double parameters and double return type.
        /// </summary>
        /// <param name="term">Traget term</param>
        /// <returns>MethodInfo of found class or null</returns>
        IResolvedTerm FindFunction(IFunctionDeclarationTerm term);

        /// <summary>
        /// Find property with target name and double return type.
        /// </summary>
        /// <param name="term">Traget term</param>
        /// <returns>PropertyInfo of found class or null</returns>
        IResolvedTerm FindConstant(IConstantDeclarationTerm term);
    }
}