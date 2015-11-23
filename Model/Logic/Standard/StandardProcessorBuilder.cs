using AnsiSoft.Calculator.Model.Analyzer.Facade;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Translate;
using AnsiSoft.Calculator.Model.Interface.Facade;

namespace AnsiSoft.Calculator.Model.Logic.Standard
{
    /// <summary>
    /// Class for create processor build with standard preference
    /// </summary>
    public static partial class StandardProcessorBuilder
    {
        /// <summary>
        /// Create <see cref="ProcessorBuilder"/> class with standard preferences.
        /// </summary>
        /// <param name="linkedLibraryFactory">Linked class</param>
        /// <returns>Builder for processor</returns>
        public static IProcessorBuilder CreateProcessorBuilder(ILinkedLibraryFactory linkedLibraryFactory) =>
            new ProcessorBuilder()
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
