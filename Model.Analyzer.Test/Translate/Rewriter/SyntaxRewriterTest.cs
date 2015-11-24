using System;
using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Rewriter
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class SyntaxRewriterTest
    {
        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Filter_FilterPredicateWithTargetTarget_TargetResult(bool result)
        {
            var node = MockRepository.GenerateStub<ISyntacticNode>();
            var rewrittenNode = MockRepository.GenerateStub<ISyntacticNode>();
            Func<ISyntacticNode, bool> filterPredicate = nd => result;
            Func<ISyntacticNode, IEnumerable<ISyntacticNode>, ISyntacticNode> visitor = 
                (nd, ch) => rewrittenNode;
            var rewriter = new SyntaxRewriter(filterPredicate, visitor);
            Assert.That(rewriter.Filter(node), Is.EqualTo(result));
        }

        public void Visit_SomeVisitor_VisitorResult()
        {
            var node = MockRepository.GenerateStub<ISyntacticNode>();
            var rewrittenNode = MockRepository.GenerateStub<ISyntacticNode>();
            Func<ISyntacticNode, bool> filterPredicate = nd => true;
            Func<ISyntacticNode, IEnumerable<ISyntacticNode>, ISyntacticNode> visitor =
                (nd, ch) => rewrittenNode;
            var rewriter = new SyntaxRewriter(filterPredicate, visitor);
            Assert.That(rewriter.Visit(node, null), Is.SameAs(rewrittenNode));
        }

    }
}
