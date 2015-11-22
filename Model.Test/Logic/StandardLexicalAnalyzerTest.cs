using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Logic.Standard;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Logic
{
    [TestFixture]
    public class StandardLexicalAnalyzerTest
    {
        private LexicalAnalyzer Analyzer { get; set; }

        [SetUp]
        public void SetUp()
        {
            Analyzer = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
        }

        [Test, Description("No exception if use standard rules")]
        public void Countructor_StandardRules_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules));
        }


        [Test]
        [TestCase(@"1+2")]
        [TestCase(@"1-2*4")]
        [TestCase(@"1-2*4*")]
        [TestCase(@"1-2*4* ")]
        [TestCase(@"1-2*4*(2-4)")]
        [TestCase(@"1/2")]
        [TestCase(@"func")]
        [TestCase(@"(")]
        [TestCase(@"func (")]
        [TestCase(@"func(")]
        [TestCase(@"func(1,3)")]
        [TestCase(@"1*func(1,3)")]
        [TestCase(@"-6 * 2e-8  / (1+f)")]
        [TestCase(@"-6.3 * 2e-8  / (((1+f)")]
        public void Parse_ValidLexicalExpression_DoesNotThrow(string text)
        {
            Assert.DoesNotThrow(() => Analyzer.Parse(text));
        }

        [Test]
        [TestCase(@"1+%2")]
        [TestCase(@"1 + 2..7")]
        [ExpectedException(typeof (LexicalParsingException))]
        public void Parse_InValidLexicalExpression_Throw(string text)
        {
            Analyzer.Parse(text);
        }

        [Test]
        [TestCase(@" ", 0)]
        [TestCase(@"1+2", 3)]
        [TestCase(@"1-2*4*(2-4)", 11)]
        [TestCase(@"1/2", 3)]
        [TestCase(@"func", 1)]
        [TestCase(@"(", 1)]
        [TestCase(@"func (", 2)]
        [TestCase(@"func(", 2)]
        [TestCase(@"func(1,3)", 6)]
        [TestCase(@"1*func(1,3)", 8)]
        [TestCase(@"-6 * 2e-8  / (1+f)", 10)]
        [TestCase(@"-6.3 * 2e-8  / (((1+f)", 12)]
        public void Parse_ValidLexicalExpression_TargetTokenCount(string text, int count)
        {
            Assert.That(Analyzer.Parse(text).Count(), Is.EqualTo(count));
        }

        [Test, Description("Rignt construction")]
        public void Parse_Text_TrueToken()
        {
            var analyzer = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            const string text = "2 +3.3 + func(4,7)*u ";
            var tokens = analyzer.Parse(text).ToArray();

            Assert.That(tokens.Length, Is.EqualTo(12));

            Assert.That(tokens[0], Is.TypeOf<NumberToken>());
            Assert.That(tokens[1], Is.TypeOf<OperatorToken>());
            Assert.That(tokens[2], Is.TypeOf<NumberToken>());
            Assert.That(tokens[3], Is.TypeOf<OperatorToken>());
            Assert.That(tokens[4], Is.TypeOf<IdentifierToken>());
            Assert.That(tokens[5], Is.TypeOf<LeftBracketToken>());
            Assert.That(tokens[6], Is.TypeOf<NumberToken>());
            Assert.That(tokens[7], Is.TypeOf<SeparatorToken>());
            Assert.That(tokens[8], Is.TypeOf<NumberToken>());
            Assert.That(tokens[9], Is.TypeOf<RightBracketToken>());
            Assert.That(tokens[10], Is.TypeOf<BinaryOperatorToken>());
            Assert.That(tokens[11], Is.TypeOf<IdentifierToken>());
        }

    }
}
