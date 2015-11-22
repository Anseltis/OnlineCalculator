using AnsiSoft.Calculator.Model.Analyzer.Facade.Standard;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Exceptions;
using AnsiSoft.Calculator.Model.Interface.Facade;
using NUnit.Framework;
using static AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes.SyntacticNodeTypeHelper;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Syntactic
{

    [TestFixture]
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
            SyntacticAnalyzer.Parse(tokens, BlockOf<ExpressionBlock>());
        }


    }
}
