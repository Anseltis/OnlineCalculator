using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Translate;
using AnsiSoft.Calculator.Model.Logic.Standard;
using AnsiSoft.Calculator.Model.Reflection;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Logic
{
    [TestFixture]
    public class StandardCompilatorTest
    {
        [Test]
        [TestCase("2*(1+4)", 10)]
        [TestCase("3-1-1", 1)]
        [TestCase("PI", 3.14159)]
        [TestCase("Sin(0)", 0)]
        [TestCase("Sin(1.57)", 1)]
        [TestCase("Max(1,2+5,PI/PI+7)", 8)]
        [TestCase("1/0", double.PositiveInfinity)]
        public void Compile_Expressiion_RightResult(string text, double value)
        {
            var linkedLibraryFactory = new StaticLinkedLibraryFactory(typeof(StandardProcessorBuilder.LinkedMath));

            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);
            var linker = new Linker(StandardProcessorBuilder.LinkerRules, 
                linkedLibraryFactory.CreateLinkedLibrary());
            var tokens = lexical.Parse(text);
            var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
            var translatedTree = translator.Translate(tree);
            translator.CheckResult(translatedTree);
            var linkedTree = linker.Resolve(translatedTree);
            linker.CheckResult(linkedTree);
            var compilator = new Compilator();
            var lambda = compilator.CreateExpression(linkedTree);
            var result = lambda.Compile()();
            Assert.That(result, Is.EqualTo(value).Within(1e-1));
        }

    }
}
