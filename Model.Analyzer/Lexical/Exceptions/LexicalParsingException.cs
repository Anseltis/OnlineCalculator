using System;
using System.Runtime.Serialization;

namespace AnsiSoft.Calculator.Model.Analyzer.Lexical.Exceptions
{
    /// <summary>
    /// Exception class for wrong leical parsing of expression
    /// This case reveals if expression doesn't pass lexical analyzer
    /// </summary>
    [Serializable]
    public sealed class LexicalParsingException : Exception
    {
        /// <summary>
        /// Wrog exprexxion which doesn't pass lexical analyzer
        /// </summary>
        public string Expression { get; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="LexicalParsingException"/> class.
        /// </summary>
        /// <param name="expression">Wrong expression</param>
        public LexicalParsingException(string expression) :
            base($"Can't parse expression '{expression}'")
        {
            Expression = expression;
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
            info.AddValue(nameof(Expression), Expression);
            base.GetObjectData(info, context);
        }

    }
}
