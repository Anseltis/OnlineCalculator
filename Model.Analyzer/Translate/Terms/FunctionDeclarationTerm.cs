namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for function term without real linking
    /// </summary>
    public sealed class FunctionDeclarationTerm : IDeclarationTerm
    {
        /// <summary>
        /// Function name
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionDeclarationTerm"/> class.
        /// </summary>
        /// <param name="identifier">Function name</param>
        public FunctionDeclarationTerm(string identifier)
        {
            Identifier = identifier;
        }

        
    }
}
