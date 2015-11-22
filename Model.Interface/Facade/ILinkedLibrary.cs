using System.Reflection;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for external source of resolving constants and functions
    /// </summary>
    public interface ILinkedLibrary
    {
        /// <summary>
        /// Find method with target name, double parameters, double return type and last param adouble argument.
        /// </summary>
        /// <param name="name">Target method name</param>
        /// <param name="argumentCount">Argument count</param>
        /// <returns>MethodInfo of found class or null</returns>
        MethodInfo FindParamMethod(string name, int argumentCount);

        /// <summary>
        /// Find method with target name, double parameters and double return type.
        /// </summary>
        /// <param name="name">Target method name</param>
        /// <param name="argumentCount">Argument count</param>
        /// <returns>MethodInfo of found class or null</returns>
        MethodInfo FindMethod(string name, int argumentCount);

        /// <summary>
        /// Find property with target name and double return type.
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>PropertyInfo of found class or null</returns>
        PropertyInfo FindProperty(string name);
    }
}