using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Translate;
using AnsiSoft.Calculator.Model.Logic.Standard;
using AnsiSoft.Calculator.Model.Reflection;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Logic.Standard
{
    [TestFixture]
    [Category("Standard realization")]
    public class StandardLinkerTest
    {

        [Test]
        [TestCase("2*(1+4)")]
        [TestCase("2*(1+PI)")]
        [TestCase("Sin(1+4)")]
        [TestCase("Max(1+4,2,4,8)")]
        public void Resolve_Expressiion_DoesNotThrow(string text)
        {
            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);

            var linkedLibraryFactory = new StaticLinkedLibraryFactory(typeof(StandardProcessorBuilder.LinkedMath));
            var linker = new Linker(StandardProcessorBuilder.LinkerRules, linkedLibraryFactory.CreateLinkedLibrary());
            Assert.DoesNotThrow(
                () =>
                {
                    var tokens = lexical.Parse(text);
                    var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
                    var translatedTree = translator.Translate(tree);
                    translator.CheckResult(translatedTree);
                    var linkedTree = linker.Resolve(translatedTree);
                    linker.CheckResult(linkedTree);
                });
        }

        [Test]
        [TestCase("Pix")]
        [TestCase("Pi")]
        [TestCase("Sen(1+4)")]
        [ExpectedException(typeof (CannotResolveIdentifierException))]
        public void Resolve_UnresolverIdentifier_Throw(string text)
        {
            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);
            var linkedLibraryFactory = new StaticLinkedLibraryFactory(typeof(StandardProcessorBuilder.LinkedMath));
            var linker = new Linker(StandardProcessorBuilder.LinkerRules, linkedLibraryFactory.CreateLinkedLibrary());

            var tokens = lexical.Parse(text);
            var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
            var translatedTree = translator.Translate(tree);
            translator.CheckResult(translatedTree);
            linker.Resolve(translatedTree);

        }

    }
}
