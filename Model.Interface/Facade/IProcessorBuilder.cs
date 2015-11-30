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
        ILexicalAnalyzer LexicalAnalyzer { get; }

        /// <summary>
        /// Syntactic analyzer
        /// </summary>
        ISyntacticAnalyzer SyntacticAnalyzer { get; }

        /// <summary>
        /// Target for syntactic analyzer
        /// </summary>
        ISyntacticNodeType SyntacticTarget { get; }

        /// <summary>
        /// Translator 
        /// </summary>
        ITranslator Translator { get;  }

        /// <summary>
        /// Identifier reslver
        /// </summary>
        ILinker Linker { get; }

        /// <summary>
        /// Expression builder
        /// </summary>
        ICompiler Compiler { get; }
    }
}