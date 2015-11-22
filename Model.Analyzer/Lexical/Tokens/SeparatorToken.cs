using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens
{
    /// <summary>
    /// Token class for separator
    /// </summary>
    public sealed class SeparatorToken : Token
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="SeparatorToken"/> class.
        /// </summary>
        /// <param name="builder">Token builder</param>
        public SeparatorToken(ITokenBuilder builder) : base(builder)
        {
        }
    }
}
