using System;
using System.Runtime.Serialization;

namespace AnsiSoft.Calculator.Model.Analyzer.Exceptions
{
    /// <summary>
    /// Exception class for wrong lexical rules
    /// This case reveals if don't reduce tail length.
    /// </summary>
    [Serializable]
    public sealed class WrongLexicalRuleException : Exception
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="WrongLexicalRuleException"/> class.
        /// </summary>
        /// <param name="pattern">Expression pattern</param>
        public WrongLexicalRuleException(string pattern) : base($"Wrong lexical rule '{pattern}'")
        {
            Pattern = pattern;
        }

        /// <summary>
        /// regular expression of lexical rule
        /// </summary>
        public string Pattern { get; }

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
            info.AddValue(nameof(Pattern), Pattern);
            base.GetObjectData(info, context);
        }


    }
}
