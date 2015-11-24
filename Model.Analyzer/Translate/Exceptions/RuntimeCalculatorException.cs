using System;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions
{
    /// <summary>
    /// Exception class for runtime error in the calculation expression
    /// This case reveals if expression doesn't pass translation
    /// </summary>
    [Serializable]
    public sealed class RuntimeCalculatorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeCalculatorException"/> class.
        /// </summary>
        /// <param name="innerException"></param>
        public RuntimeCalculatorException(Exception innerException) : 
            base($"Runtime exception: {innerException.Message}.", innerException)
        {
        }
    }
}
