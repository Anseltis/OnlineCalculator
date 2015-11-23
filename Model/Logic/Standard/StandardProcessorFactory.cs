using System;
using AnsiSoft.Calculator.Model.Analyzer;
using AnsiSoft.Calculator.Model.Analyzer.Facade;
using AnsiSoft.Calculator.Model.Interface.Facade;

namespace AnsiSoft.Calculator.Model.Logic.Standard
{
    /// <summary>
    /// 
    /// </summary>
    public class StandardProcessorFactory : IProcessorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IProcessor CreateProcessor() => new Processor(ProcessorBuilder);

        /// <summary>
        /// 
        /// </summary>
        public IProcessorBuilder ProcessorBuilder { get; } 

        /// <summary>
        /// 
        /// </summary>
        public StandardProcessorFactory(ILinkedLibraryFactory linkedLibraryFactory)
        {
            ProcessorBuilder = StandardProcessorBuilder.CreateProcessorBuilder(linkedLibraryFactory);
        }
    }
}
