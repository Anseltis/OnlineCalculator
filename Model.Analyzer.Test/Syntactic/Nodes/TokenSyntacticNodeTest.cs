using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Syntactic.Nodes
{
    [TestFixture]
    [Category("Syntactic analyzer")]
    public class TokenSyntacticNodeTest
    {
        [Test]
        public void Nodes_DoNothing_IsEmpty()
        {
            var token = MockRepository.GenerateStub<IToken>();
            var tokenNode = new TokenSyntacticNode(token);
            Assert.That(tokenNode.Nodes.Any(),Is.EqualTo(false));
        }

        [Test]
        public void Rewriter_SomeList_IsEmpty()
        {
            var token = MockRepository.GenerateStub<IToken>();
            var tokenNode = new TokenSyntacticNode(token);
            var rewriteNodes = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
                MockRepository.GenerateStub<ISyntacticNode>()
            };
            var rewriteTokenNode = tokenNode.Rewrite(rewriteNodes);
            Assert.That(rewriteTokenNode.Nodes.Any(), Is.EqualTo(false));
        }

    }
}
