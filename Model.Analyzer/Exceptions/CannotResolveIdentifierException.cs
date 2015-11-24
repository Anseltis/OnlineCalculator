using System;
using System.Runtime.Serialization;

namespace AnsiSoft.Calculator.Model.Analyzer.Exceptions
{
    /// <summary>
    /// Exception class with error resolve identifier
    /// This case reveals if expression contain identifiers which don't find in liked files
    /// </summary>
    [Serializable]
    public sealed class CannotResolveIdentifierException : Exception
    {
        public string Identifier { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CannotResolveIdentifierException"/> class.
        /// </summary>
        /// <param name="identifier">Unresolved identifier</param>
        public CannotResolveIdentifierException(string identifier) :
            base($"Unresolves identifier '{identifier}' (or wrong signature)")
        {
            Identifier = identifier;
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            info.AddValue(nameof(Identifier), Identifier);
            base.GetObjectData(info, context);
        }

    }
}
