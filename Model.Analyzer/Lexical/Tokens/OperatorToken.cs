using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens
{
    /// <summary>
    /// Token class for universal (unary and binary) operator
    /// </summary>
    public sealed class OperatorToken : Token
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="OperatorToken"/> class.
        /// </summary>
        /// <param name="builder">Token builder</param>
        public OperatorToken(ITokenBuilder builder) : base(builder)
        {
        }
    }
}
