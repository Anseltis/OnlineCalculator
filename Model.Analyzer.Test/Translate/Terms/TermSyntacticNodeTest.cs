using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    public class TermSyntacticNodeTest
    {
        [Test]
        public void Nodes_Empty_Empty()
        {
            var term = MockRepository.GenerateStub<ITerm>();
            var nodes = Enumerable.Empty<ISyntacticNode>();
            var termNode = new TermSyntacticNode(term, nodes);
            Assert.That(termNode.Nodes, Is.SameAs(nodes));
        }

        [Test]
        public void Nodes_Null_Null()
        {
            var term = MockRepository.GenerateStub<ITerm>();
            var termNode = new TermSyntacticNode(term, null);
            Assert.That(termNode.Nodes, Is.Null);
        }

        [Test]
        public void Nodes_SomeList_Same()
        {
            var term = MockRepository.GenerateStub<ITerm>();
            var nodes = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
                MockRepository.GenerateStub<ISyntacticNode>()
            };
            var termNode = new TermSyntacticNode(term, nodes);
            Assert.That(termNode.Nodes, Is.SameAs(nodes));
        }

        [Test]
        public void Term_BlockStub_Same()
        {
            var term = MockRepository.GenerateStub<ITerm>();
            var nodes = Enumerable.Empty<ISyntacticNode>();
            var termNode = new TermSyntacticNode(term, nodes);
            Assert.That(termNode.Term, Is.SameAs(term));
        }

        [Test]
        public void Rewriter_SomeList_Same()
        {
            var term = MockRepository.GenerateStub<ITerm>();
            var nodes = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
                MockRepository.GenerateStub<ISyntacticNode>()
            };
            var termNode = new TermSyntacticNode(term, nodes);
            var rewriteNodes = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
                MockRepository.GenerateStub<ISyntacticNode>()
            };
            var rewriteTermNode = termNode.Rewrite(rewriteNodes);
            Assert.That(rewriteTermNode.Nodes, Is.SameAs(rewriteNodes));
        }
    }
}
