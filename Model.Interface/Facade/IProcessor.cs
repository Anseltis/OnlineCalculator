using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Interface.Facade
{
    /// <summary>
    /// Facade interface for calculator
    /// </summary>
    public interface IProcessor
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
        ITranslator Translator { get; }

        /// <summary>
        /// Identifier reslver
        /// </summary>
        ILinker Linker { get; }

        /// <summary>
        /// Expression builder
        /// </summary>
        ICompilator Compilator { get; }

        /// <summary>
        /// Calculate text expression
        /// </summary>
        /// <param name="text">Text expression</param>
        /// <returns>Result value</returns>
        double Calculate(string text);
    }
}