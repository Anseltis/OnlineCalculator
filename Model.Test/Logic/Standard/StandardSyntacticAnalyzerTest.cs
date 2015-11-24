using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Logic.Standard;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Logic.Standard
{

    [TestFixture]
    [Category("Standard realization")]
    public class StandardSyntacticAnalyzerTest
    {
        private ILexicalAnalyzer LexicalAnalyzer { get; set; }
        private ISyntacticAnalyzer SyntacticAnalyzer { get; set; }

        [SetUp]
        public void SetUp()
        {
            LexicalAnalyzer = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            SyntacticAnalyzer = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);
        }

        [Test]
        [TestCase(@"1+2")]
        [TestCase(@"1+2*func(3+4,-7)")]
        [TestCase(@"1+2*3")]
        [TestCase(@"sin(cos(alpha))")]
        public void Parse_ValidLexicalExpression_DoesNotThrow(string text)
        {
            Assert.DoesNotThrow(
                () =>
                {
                    var tokens = LexicalAnalyzer.Parse(text);
                    SyntacticAnalyzer.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
                });
        }

        [Test]
        [TestCase(@"")]
        [TestCase(@"1+")]
        [TestCase(@"2.7+3*(1")]
        [TestCase(@"func()")]
        [ExpectedException(typeof(SyntacticParseException))]
        public void Parse_InValidLexicalExpression_Throw(string text)
        {
            var tokens = LexicalAnalyzer.Parse(text);
            SyntacticAnalyzer.Parse(tokens, SyntacticNodeTypeHelper.BlockOf<ExpressionBlock>());
        }


    }
}
