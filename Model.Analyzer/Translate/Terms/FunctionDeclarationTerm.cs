using System;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for function term without real linking
    /// </summary>
    public sealed class FunctionDeclarationTerm : IFunctionDeclarationTerm
    {
        #region inplement IDeclarationTerm
        /// <summary>
        /// Function name
        /// </summary>
        public string Identifier { get; }
        #endregion

        #region implement IFunctionDeclarationTerm
        /// <summary>
        /// Argument count for function
        /// </summary>
        public int ArgumentCount { get; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionDeclarationTerm"/> class.
        /// </summary>
        /// <param name="identifier">Function name</param>
        /// <param name="argumentCount">Argument count for function</param>
        public FunctionDeclarationTerm(string identifier, int argumentCount)
        {
            if (identifier == null)
            {
                throw new ArgumentNullException(nameof(identifier));
            }
            if (argumentCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(argumentCount));
            }
            Identifier = identifier;
            ArgumentCount = argumentCount;
        }

        
    }
}
