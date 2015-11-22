namespace AnsiSoft.Calculator.Model.Interface.Nodes
{
    /// <summary>
    /// Interface for terminal block of synactic analyzer's parsing result (tokens)
    /// </summary>
    public interface ITokenSyntacticNode : ISyntacticNode
    {
        /// <summary>
        /// Token from source token sequence
        /// </summary>
        IToken Token { get; }
    }
}