using System;
using System.Runtime.Serialization;

namespace AnsiSoft.Calculator.Model.ReflectionTool
{
    /// <summary>
    /// Exception class of runtime compile error
    /// This case reveals if source code has any error
    /// </summary>
    [Serializable]
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
        /// <param name="innerException">Inner exception</param>
        public RuntimeCompileException(string sourceCode, Exception innerException) : 
            base("Error during runtime compilation", innerException)
        {
            SourceCode = sourceCode;
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
            info.AddValue(nameof(SourceCode), SourceCode);
            base.GetObjectData(info, context);
        }

    }
}
