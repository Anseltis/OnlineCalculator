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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        double Calculate(string text);
    }
}