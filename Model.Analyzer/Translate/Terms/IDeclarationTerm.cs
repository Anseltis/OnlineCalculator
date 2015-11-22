using System.Security.Cryptography.X509Certificates;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
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
