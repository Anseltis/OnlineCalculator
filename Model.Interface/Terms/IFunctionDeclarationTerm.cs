namespace AnsiSoft.Calculator.Model.Interface.Terms
{
    public interface IFunctionDeclarationTerm : IDeclarationTerm
    {
        /// <summary>
        /// Argument count for function
        /// </summary>
        int ArgumentCount { get; }
    }
}