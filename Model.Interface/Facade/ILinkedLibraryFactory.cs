namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    public interface ILinkedLibraryFactory
    {
        /// <summary>
        /// Create linked library
        /// </summary>
        /// <returns>Linked library</returns>
        ILinkedLibrary CreateLinkedLibrary();
    }
}