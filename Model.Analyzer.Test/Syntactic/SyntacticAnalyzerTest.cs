using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Syntactic
{
    [TestFixture]
    [Category("Syntactic analyzer")]
    class SyntacticAnalyzerTest
    {
        public void Rules_Null_Null()
        {
            var analyzer = new SyntacticAnalyzer(null);
            Assert.That(analyzer.Rules, Is.Null);
        }

        [Test]
        public void Rules_Empty_Empty()
        {
            var rules = Enumerable.Empty<IBlock>();
            var analyzer = new SyntacticAnalyzer(rules);
            Assert.That(analyzer.Rules, Is.SameAs(rules));
        }

        [Test]
        public void Rules_SomeList_Same()
        {
            var rules = new[]
            {
                MockRepository.GenerateStub<IBlock>(),
                MockRepository.GenerateStub<IBlock>()
            };
            var analyzer = new SyntacticAnalyzer(rules);
            Assert.That(analyzer.Rules, Is.SameAs(rules));
        }
    }
}
