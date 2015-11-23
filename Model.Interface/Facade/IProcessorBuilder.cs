using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Interface for help to create processor-like classes
    /// </summary>
    public interface IProcessorBuilder
    {
        /// <summary>
        /// Lexical analyzer
        /// </summary>
        ILexicalAnalyzer LexicalAnalyzer { get; set; }

        /// <summary>
        /// Syntactic analyzer
        /// </summary>
        ISyntacticAnalyzer SyntacticAnalyzer { get; set; }

        /// <summary>
        /// Target for syntactic analyzer
        /// </summary>
        ISyntacticNodeType SyntacticTarget { get; set; }

        /// <summary>
        /// Translator 
        /// </summary>
        ITranslator Translator { get; set; }

        /// <summary>
        /// Identifier reslver
        /// </summary>
        ILinker Linker { get; set; }

        /// <summary>
        /// Expression builder
        /// </summary>
        ICompilator Compilator { get; set; }
    }
}