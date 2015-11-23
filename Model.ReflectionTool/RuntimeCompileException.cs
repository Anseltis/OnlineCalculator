using System;

namespace AnsiSoft.Calculator.Model.ReflectionTool
{
    /// <summary>
    /// Exception class of runtime compile error
    /// This case reveals if source code has any error
    /// </summary>
    public class RuntimeCompileException : Exception
    {
        /// <summary>
        /// Source code
        /// </summary>
        public string SourceCode { get; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="RuntimeCompileException"/> class.
        /// </summary>
        /// <param name="sourceCode">Source code</param>
        public RuntimeCompileException(string sourceCode) : base("Error during runtime compilation")
        {
            SourceCode = sourceCode;
        }


    }
}
