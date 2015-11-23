using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Interface.Resolvers
{
    /// <summary>
    /// Interface for resolve type
    /// </summary>
    public interface IResolverType
    {
        /// <summary>
        /// Check aviability of resolve term
        /// </summary>
        /// <param name="term">Tern in syntactic tree</param>
        /// <returns>Aviability of resolve term</returns>
        bool Resolve(ITerm term);
    }
}