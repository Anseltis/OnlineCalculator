using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;

namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers
{
    /// <summary>
    /// Interface for resolve type T term
    /// </summary>
    /// <typeparam name="T">Type term</typeparam>
    public sealed class ResolverType<T> : IResolverType where T: IDeclarationTerm
    {
        #region implement IResolverType
        public bool Resolve(ITerm term)
        {
            return term is T;
        }
        #endregion
    }
}
