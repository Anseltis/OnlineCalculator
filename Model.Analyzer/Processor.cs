using System;
using AnsiSoft.Calculator.Model.Analyzer.Facade;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
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
        public ILexicalAnalyzer LexicalAnalyzer { get; }
        public ISyntacticAnalyzer SyntacticAnalyzer { get;}
        public ISyntacticNodeType SyntacticTarget { get; }
        public ITranslator Translator { get; }
        public ILinker Linker { get; }
        public ICompilator Compilator { get;  }

        /// <summary>
        /// Calculate expression text.
        /// </summary>
        /// <param name="text">Expression text</param>
        /// <returns>Result value</returns>
        /// <exception cref="RuntimeCalculatorException">Thrown when raise runtime error.</exception>
        public double Calculate(string text)
        {
            var tokens = LexicalAnalyzer.Parse(text);
            var tree = SyntacticAnalyzer.Parse(tokens, SyntacticTarget);
            var translatedTree = Translator.Translate(tree);
            Translator.CheckResult(translatedTree);
            var linkedTree = Linker.Resolve(translatedTree);
            Linker.CheckResult(linkedTree);
            var lambda = Compilator.CreateExpression(linkedTree);

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
            if (builder.Compilator == null)
            {
                throw new ArgumentNullException(nameof(builder.Compilator));
            }

            LexicalAnalyzer = builder.LexicalAnalyzer;
            SyntacticAnalyzer = builder.SyntacticAnalyzer;
            SyntacticTarget = builder.SyntacticTarget;
            Translator = builder.Translator;
            Linker = builder.Linker;
            Compilator = builder.Compilator;
        }
    }
}
