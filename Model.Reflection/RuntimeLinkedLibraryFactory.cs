using System;
using System.Linq;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Reflection.Exceptions;
using AnsiSoft.Calculator.Model.ReflectionTool;

namespace AnsiSoft.Calculator.Model.Reflection
{
    /// <summary>
    /// Class for create linked library from text with runtime compilation
    /// </summary>
    public sealed class RuntimeLinkedLibraryFactory : ILinkedLibraryFactory
    {
        #region implement ILinkedLibraryFactory
        /// <summary>
        /// Create linked library
        /// </summary>
        /// <returns>Linked library</returns>
        public ILinkedLibrary CreateLinkedLibrary() => new LinkedLibrary(TypeLazy);
        #endregion

        /// <summary>
        /// Source code of static class
        /// </summary>
        public string SourceCode { get; }

        /// <summary>
        /// Referenced assemblies
        /// </summary>
        public string[] Assemblies { get; }

        /// <summary>
        /// Lazy object with the result for safe multithread work
        /// </summary>
        public Lazy<Type> TypeLazy { get; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="RuntimeLinkedLibraryFactory"/> class.
        /// </summary>
        /// <param name="sourceCode">Source code</param>
        /// <param name="assemblies">Referenced assemblies</param>
        /// <exception cref="ArgumentNullException">Throw if source code or assemplies are null</exception>
        public RuntimeLinkedLibraryFactory(string sourceCode, string[] assemblies)
        {
            if (sourceCode == null)
            {
                throw new ArgumentNullException(nameof(sourceCode));
            }
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            SourceCode = sourceCode;
            Assemblies = assemblies;
            TypeLazy = new Lazy<Type>(() =>
            {
                var type = CompilerHelper.Compile(SourceCode, Assemblies)
                    .FirstOrDefault(t => t.IsStatic());

                if (type == null)
                {
                    throw new NonStaticClassException();
                }

                return type;
            });
        }

        public static string[] StandardAssemblies { get; } = {"System.Core.dll"};

        /// <summary>
        ///  Initializes a new instance of the <see cref="RuntimeLinkedLibraryFactory"/> class.
        /// </summary>
        /// <param name="sourceCode">Source code</param>
        public RuntimeLinkedLibraryFactory(string sourceCode) : this(sourceCode, StandardAssemblies)
        {
        }


    }
}
