using System;
using AnsiSoft.Calculator.Model.Analyzer;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Translate;
using AnsiSoft.Calculator.Model.Interface.Facade;
using static AnsiSoft.Calculator.Model.Logic.Standard.StandardProcessorBuilder;

namespace AnsiSoft.Calculator.Model.Logic.Standard
{
    /// <summary>
    /// Class-factory for create IProcessor with standard preferences
    /// </summary>
    public class StandardProcessorFactory : IProcessorFactory
    {
        #region implement IProcessorFactory
        public IProcessor CreateProcessor() => new Processor(ProcessorBuilder);
        #endregion
        /// <summary>
        /// Class for create<see cref="IProcessor"/> class
        /// </summary>
        public IProcessorBuilder ProcessorBuilder { get; }

        /// <summary>
        /// Create <see cref="ProcessorBuilder"/> class with standard preferences.
        /// </summary>
        /// <param name="linkedLibraryFactory">Linked class</param>
        /// <exception cref="ArgumentNullException">Throw if linked library factory is null</exception>
        public StandardProcessorFactory(ILinkedLibraryFactory linkedLibraryFactory)
        {
            if (linkedLibraryFactory == null)
            {
                throw new ArgumentNullException(nameof(linkedLibraryFactory));
            }
            ProcessorBuilder = new ProcessorBuilder()
            {
                LexicalAnalyzer = new LexicalAnalyzer(LexicalRules),
                SyntacticAnalyzer = new SyntacticAnalyzer(SyntacticRules),
                SyntacticTarget = SyntacticTarget,
                Translator = new Translator(TranslateRules),
                Linker = new Linker(LinkerRules, linkedLibraryFactory.CreateLinkedLibrary()),
                Compilator = new Compilator()
            };
        }
    }
}
