namespace AnsiSoft.Calculator.Model.Interface.Terms
{

    /// <summary>
    /// Interface for terms without real linking
    /// </summary>
    public interface IDeclarationTerm : ITerm
    {
        /// <summary>
        /// String of unresolved identifier
        /// </summary>
        string Identifier { get; }
    }
}
