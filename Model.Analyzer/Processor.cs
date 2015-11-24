using System;
using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;

namespace AnsiSoft.Calculator.Model.Analyzer
{
    /// <summary>
    /// Facade class for calculator
    /// </summary>
    public sealed class Processor : IProcessor
    {
        #region implement IProcessor
        /// <summary>
        /// Calculate text expression.
        /// </summary>
        /// <param name="text">Text expression</param>
        /// <returns>Result value</returns>
        /// <exception cref="RuntimeCalculatorException">Throw when raise runtime error.</exception>
        public double Calculate(string text)
        {
            var tokens = LexicalAnalyzer.Parse(text);
            var tree = SyntacticAnalyzer.Parse(tokens, SyntacticTarget);
            var translatedTree = Translator.Translate(tree);
            Translator.CheckResult(translatedTree);
            var linkedTree = Linker.Resolve(translatedTree);
            Linker.CheckResult(linkedTree);
            var lambda = Compiler.CreateExpression(linkedTree);

            try
            {
                return lambda.Compile()();
            }
            catch (Exception exception)
            {
                throw new RuntimeCalculatorException(exception);
            }
        }
        #endregion

        /// <summary>
        /// Lexical analyzer
        /// </summary>
        public ILexicalAnalyzer LexicalAnalyzer { get; }

        /// <summary>
        /// Syntactic analyzer
        /// </summary>
        public ISyntacticAnalyzer SyntacticAnalyzer { get; }

        /// <summary>
        /// Target for syntactic analyzer
        /// </summary>
        public ISyntacticNodeType SyntacticTarget { get; }

        /// <summary>
        /// Translator 
        /// </summary>
        public ITranslator Translator { get; }

        /// <summary>
        /// Identifier reslver
        /// </summary>
        public ILinker Linker { get; }

        /// <summary>
        /// Expression builder
        /// </summary>
        public ICompiler Compiler { get; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        /// <param name="builder">Builder class</param>
        /// <exception cref="ArgumentNullException">Throw if builder properties are null</exception>
        public Processor(IProcessorBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.LexicalAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(builder.LexicalAnalyzer));
            }
            if (builder.SyntacticAnalyzer == null)
            {
                throw new ArgumentNullException(nameof(builder.SyntacticAnalyzer));
            }
            if (builder.SyntacticTarget == null)
            {
                throw new ArgumentNullException(nameof(builder.SyntacticTarget));
            }
            if (builder.Translator == null)
            {
                throw new ArgumentNullException(nameof(builder.Translator));
            }
            if (builder.Linker == null)
            {
                throw new ArgumentNullException(nameof(builder.Linker));
            }
            if (builder.Compiler == null)
            {
                throw new ArgumentNullException(nameof(builder.Compiler));
            }

            LexicalAnalyzer = builder.LexicalAnalyzer;
            SyntacticAnalyzer = builder.SyntacticAnalyzer;
            SyntacticTarget = builder.SyntacticTarget;
            Translator = builder.Translator;
            Linker = builder.Linker;
            Compiler = builder.Compiler;
        }
    }
}
