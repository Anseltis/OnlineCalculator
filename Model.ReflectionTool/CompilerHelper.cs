using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace AnsiSoft.Calculator.Model.ReflectionTool
{
    public static class CompilerHelper
    {
        /// <summary>
        /// Runtime compile source code.
        /// </summary>
        /// <param name="text">Source code</param>
        /// <param name="assemblies">Referenced assemblies</param>
        /// <returns>List of types</returns>
        public static Type[] Compile(string text, string[] assemblies)
        {
            try
            {
                using (var compiler = new CSharpCodeProvider())
                {
                    var args = new CompilerParameters { GenerateInMemory = true };
                    args.ReferencedAssemblies.AddRange(assemblies);

                    var res = compiler.CompileAssemblyFromSource(args, text);
                    var assembly = res.CompiledAssembly;
                    return assembly.GetTypes();
                }
            }
            catch (Exception exception)
            {
                throw new RuntimeCompileException(text, exception);
            }
        }
    }
}
