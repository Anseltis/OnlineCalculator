namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for factory for create calculator
    /// </summary>
    public interface IProcessorFactory
    {
        /// <summary>
        /// Create new <see cref="IProcessor" /> instance.
        /// </summary>
        /// <returns>New processor</returns>
        IProcessor CreateProcessor();
    }
}
