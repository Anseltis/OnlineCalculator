using System;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions
{
    /// <summary>
    /// Exception class with error resolve identifier
    /// This case reveals if expression contain identifiers which don't find in liked files
    /// </summary>
    public sealed class CannotResolveIdentifierException : Exception
    {
        public string Identifier { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotResolveIdentifierException"/> class.
        /// </summary>
        /// <param name="identifier">Unresolved identifier</param>
        public CannotResolveIdentifierException(string identifier) :
            base($"Does not resolve identifier '{identifier}'")
        {
            Identifier = identifier;
        }
    }
}
