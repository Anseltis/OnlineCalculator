using AnsiSoft.Calculator.Model.Analyzer.Facade;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Logic
{
    /// <summary>
    /// Class for create <see cref="Processor"/> class
    /// </summary>
    public sealed class ProcessorBuilder : IProcessorBuilder
    {
        /// <summary>
        /// Lexical analyzer
        /// </summary>
        public ILexicalAnalyzer LexicalAnalyzer { get; set; }

        /// <summary>
        /// Syntactic analyzer
        /// </summary>
        public ISyntacticAnalyzer SyntacticAnalyzer { get; set; }

        /// <summary>
        /// Target for syntactic analyzer
        /// </summary>
        public ISyntacticNodeType SyntacticTarget { get; set; }

        /// <summary>
        /// Translator 
        /// </summary>
        public ITranslator Translator { get; set; }

        /// <summary>
        /// Identifier reslver
        /// </summary>
        public ILinker Linker { get; set; }

        /// <summary>
        /// Expression builder
        /// </summary>
        public ICompilator Compilator { get; set; }
    }
}
