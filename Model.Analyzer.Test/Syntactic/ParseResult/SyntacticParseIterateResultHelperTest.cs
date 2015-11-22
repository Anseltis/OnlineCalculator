using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.ParseResult;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Syntactic.ParseResult
{
    [TestFixture]
    class SyntacticParseIterateResultHelperTest
    {
        [Test]
        public void Iterate_EmptyNodeTypes_SingleIterate()
        {
            var iteration = new SyntacticParseIterateResult(
                Enumerable.Empty<ISyntacticNode>(),
                Enumerable.Empty<ISyntacticNodeType>(),
                Enumerable.Empty<TokenSyntacticNode>());

            var rules = Enumerable.Empty<IBlock>();
            var result = iteration.Iterate(rules).ToArray();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(iteration));
        }
    }
}
