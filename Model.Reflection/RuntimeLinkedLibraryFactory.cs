using System;
using System.Linq;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.ReflectionTool;

namespace AnsiSoft.Calculator.Model.Reflection
{
    /// <summary>
    /// Class for create linked library from text with runtime compilation
    /// </summary>
    public sealed class RuntimeLinkedLibraryFactory : ILinkedLibraryFactory
    {
        /// <summary>
        /// Source code of static class
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Referenced assemblies
        /// </summary>
        public string[] Assemblies { get; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="RuntimeLinkedLibraryFactory"/> class.
        /// </summary>
        /// <param name="text">Source code</param>
        /// <param name="assemblies">Referenced assemblies</param>
        public RuntimeLinkedLibraryFactory(string text, string[] assemblies)
        {
            Text = text;
            Assemblies = assemblies;
            TypeLazy = new Lazy<Type>(() =>
            {
                var type = CompilerHelper.Compile(Text, Assemblies)
                    .FirstOrDefault(t => t.IsStatic());

                if (type == null)
                {
                    throw new NonStaticClassException();
                }

                return type;
            });
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="RuntimeLinkedLibraryFactory"/> class.
        /// </summary>
        /// <param name="text">Source code</param>
        public RuntimeLinkedLibraryFactory(string text) : this(text, new[] { "System.Core.dll" })
        {
        }

        /// <summary>
        /// Lazy object with the result for safe multithread work
        /// </summary>
        public Lazy<Type> TypeLazy { get; }

        /// <summary>
        /// Create linked library
        /// </summary>
        /// <returns>Linked library</returns>
        public ILinkedLibrary CreateLinkedLibrary() => new LinkedLibrary(TypeLazy);
    }
}
