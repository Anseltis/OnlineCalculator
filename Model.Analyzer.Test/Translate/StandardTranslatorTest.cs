using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Facade.Standard;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Translate;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate
{
    [TestFixture]
    public class StandardTranslatorTest
    {

        [Test]
        [TestCase("func(1,2,3)")]
        [TestCase("2*(1+4)")]
        [TestCase("2*(1+4.8)")]
        [TestCase("1")]
        [TestCase("1+2")]
        [TestCase("1+2*alpha")]
        public void TranslateAndCheck_Expressiion_DoesNotThrow(string text)
        {
            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);
            Assert.DoesNotThrow(
                () =>
                {
                    var tokens = lexical.Parse(text);
                    var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
                    var result = translator.Translate(tree);
                    translator.CheckResult(result);
                });
        }

        [Test]
        public void Translate_SpecialExpression1_TargetNodeCount()
        {
            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);
            const string text = "func(1,2,3)";

            var tokens = lexical.Parse(text);
            var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
            var result = translator.Translate(tree);

            Assert.That(result.Nodes.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Translate_SpecialExpression2_TargetNodeCount()
        {
            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);
            const string text = "(((-1+1+3+pi)))";

            var tokens = lexical.Parse(text);
            var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
            var result = translator.Translate(tree);

            Assert.That(result.Nodes.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Translate_SumOfProduct_SumTerm()
        {
            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);
            const string text = "2*3+3*4.7";

            var tokens = lexical.Parse(text);
            var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
            var result = translator.Translate(tree);

            var termNode = (TermSyntacticNode) result;
            Assert.That(termNode.Term, Is.TypeOf<BinaryOperatorTerm>());
            var operatorTerm = (BinaryOperatorTerm) termNode.Term;
            Assert.That(operatorTerm.Operator, Is.TypeOf<PlusOperator>());
        }

        [Test]
        public void Translate_ProductOfSum_ProductTerm()
        {
            var lexical = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            var syntactic = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
            var translator = new Translator(StandardProcessorBuilder.TranslateRules);
            const string text = "(2+3)*(3+4.7)";

            var tokens = lexical.Parse(text);
            var tree = syntactic.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
            var result = translator.Translate(tree);

            var termNode = (TermSyntacticNode)result;
            Assert.That(termNode.Term, Is.TypeOf<BinaryOperatorTerm>());
            var operatorTerm = (BinaryOperatorTerm)termNode.Term;
            Assert.That(operatorTerm.Operator, Is.TypeOf<MultiplicationOperator>());
        }

    }
}
