using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens
{
    /// <summary>
    /// Token class for right bracket
    /// </summary>
    public sealed class RightBracketToken : Token
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="RightBracketToken"/> class.
        /// </summary>
        /// <param name="builder">Token builder</param>
        public RightBracketToken(ITokenBuilder builder) : base(builder)
        {
        }
    }
}
