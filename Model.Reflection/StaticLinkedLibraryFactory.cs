using System;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Reflection.Exceptions;
using AnsiSoft.Calculator.Model.ReflectionTool;

namespace AnsiSoft.Calculator.Model.Reflection
{
    /// <summary>
    /// Class for create linked library from static class
    /// </summary>
    public sealed class StaticLinkedLibraryFactory : ILinkedLibraryFactory
    {
        /// <summary>
        /// Class for linked library
        /// </summary>
        public Lazy<Type> TypeLazy { get; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="StaticLinkedLibraryFactory"/> class.
        /// </summary>
        /// <param name="type">Static linked class</param>
        public StaticLinkedLibraryFactory(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsStatic())
            {
                throw new NonStaticClassException(type);
            }

            TypeLazy = new Lazy<Type>(() => type);
        }

        /// <summary>
        /// Create linked library
        /// </summary>
        /// <returns>Linked library</returns>
        public ILinkedLibrary CreateLinkedLibrary()
        {
            return new LinkedLibrary<double>(TypeLazy);
        }
    }
}
