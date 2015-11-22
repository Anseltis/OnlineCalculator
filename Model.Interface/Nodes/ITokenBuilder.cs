namespace AnsiSoft.Calculator.Model.Interface.Nodes
{
    /// <summary>
    /// Builder interface for create token
    /// </summary>
    public interface ITokenBuilder
    {
        /// <summary>
        /// Lexeme text
        /// </summary>
        string Lexeme { get; set; }

        /// <summary>
        /// Space symbols before token
        /// </summary>
        string LeftTrivia { get; set; }

        /// <summary>
        /// Space symbols after token
        /// </summary>
        string RightTrivia { get; set; }
    }
}