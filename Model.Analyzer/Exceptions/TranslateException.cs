using System;

namespace AnsiSoft.Calculator.Model.Analyzer.Exceptions
{
    /// <summary>
    /// Exception class for wrong translation of expression
    /// This case reveals if expression doesn't pass translation
    /// </summary>
    [Serializable]
    public sealed class TranslateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateException"/> class.
        /// </summary>
        public TranslateException() : base("Translate error")
        {
        }
    }
}
