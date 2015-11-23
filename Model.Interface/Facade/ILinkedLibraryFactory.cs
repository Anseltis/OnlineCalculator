namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Class-factory for create <see cref="ILinkedLibrary" /> class
    /// </summary>
    public interface ILinkedLibraryFactory
    {
        /// <summary>
        /// Create linked library.
        /// </summary>
        /// <returns>Linked library</returns>
        ILinkedLibrary CreateLinkedLibrary();
    }
}