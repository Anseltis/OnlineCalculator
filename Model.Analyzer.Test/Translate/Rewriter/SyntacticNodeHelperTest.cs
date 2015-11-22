using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Rewriter
{
    [TestFixture]
    public class SyntacticNodeHelperTest
    {
        [Test]
        public void IsBlockOf_TokenNode_False()
        {
            var token = MockRepository.GenerateStub<IToken>();
            var tokenNode = new TokenSyntacticNode(token);
            Assert.That(tokenNode.IsBlockOf("some block"), Is.False);
        }

        [Test]
        public void IsBlockOf_BlockNodeWithAnotherName_False()
        {
            var block = MockRepository.GenerateStub<IBlock>();
            block.Stub(b => b.Name).Return("block1");
            var blockNode = new BlockSyntacticNode(block, Enumerable.Empty<ISyntacticNode>());
            Assert.That(blockNode.IsBlockOf("block2"), Is.False);
        }

        [Test]
        public void IsBlockOf_BlockNodeWithSameName_True()
        {
            var block = MockRepository.GenerateStub<IBlock>();
            block.Stub(b => b.Name).Return("block1");
            var blockNode = new BlockSyntacticNode(block, Enumerable.Empty<ISyntacticNode>());
            Assert.That(blockNode.IsBlockOf("block1"), Is.True);
        }

        [Test]
        public void IsBlockOf_BlockNodeWithNullName_True()
        {
            var block = MockRepository.GenerateStub<IBlock>();
            block.Stub(b => b.Name).Return(null);
            var blockNode = new BlockSyntacticNode(block, Enumerable.Empty<ISyntacticNode>());
            Assert.That(blockNode.IsBlockOf("block1"), Is.False);
        }

        [Test]
        public void Rewrite_SomeNode_RightRewriterBehavior()
        {
            var node = MockRepository.GenerateStub<ISyntacticNode>();
            node.Stub(nd => nd.Nodes).Return(Enumerable.Empty<ISyntacticNode>());
            node.Stub(nd => nd.Rewrite(Arg<IEnumerable<ISyntacticNode>>.Is.Anything))
                .Return(node);

            var rewriter = MockRepository.GenerateMock<ISyntaxRewriter>();
            rewriter.Expect(nd => nd.Filter(Arg<ISyntacticNode>.Is.Anything))
                .Return(true);
            rewriter.Expect(r => r.Visit(Arg<ISyntacticNode>.Is.Anything,
                Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(node);

            node.Rewrite(rewriter);
            rewriter.VerifyAllExpectations();
        }

        [Test]
        public void Rewrite_SomeNodeInFilter_RightNodeBehavior()
        {
            var node = MockRepository.GenerateMock<ISyntacticNode>();
            node.Expect(nd => nd.Nodes).Return(Enumerable.Empty<ISyntacticNode>());

            var rewriter = MockRepository.GenerateStub<ISyntaxRewriter>();
            rewriter.Stub(nd => nd.Filter(Arg<ISyntacticNode>.Is.Anything))
                .Return(true);
            rewriter.Stub(r => r.Visit(Arg<ISyntacticNode>.Is.Anything,
                Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(node);

            node.Rewrite(rewriter);
            node.VerifyAllExpectations();
        }

        [Test]
        public void Rewrite_SomeNodeOutOfFilter_RightNodeBehavior()
        {
            var node = MockRepository.GenerateMock<ISyntacticNode>();
            node.Expect(nd => nd.Nodes).Return(Enumerable.Empty<ISyntacticNode>());
            node.Expect(nd => nd.Rewrite(Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(node);

            var rewriter = MockRepository.GenerateStub<ISyntaxRewriter>();
            rewriter.Stub(nd => nd.Filter(Arg<ISyntacticNode>.Is.Anything))
                .Return(false);
            rewriter.Stub(r => r.Visit(Arg<ISyntacticNode>.Is.Anything,
                Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(node);

            node.Rewrite(rewriter);
            node.VerifyAllExpectations();
        }


        [Test]
        public void Rewrite_OneTrueRules_RuleResult()
        {
            var emptyNodes = Enumerable.Empty<ISyntacticNode>();
            var tree = MockRepository.GenerateStub<ISyntacticNode>();
            tree.Stub(t => t.Nodes).Return(emptyNodes);

            var tree1 = MockRepository.GenerateStub<ISyntacticNode>();

            var rule1 = MockRepository.GenerateStub<ISyntaxRewriter>();
            rule1.Stub(r => r.Filter(tree)).Return(true);
            rule1.Stub(r => r.Visit(Arg<ISyntacticNode>.Is.Anything,
                Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(tree1);

            var rules = new[] {rule1};

            Assert.That(tree.Rewrite(rules), Is.SameAs(tree1));
        }


        [Test]
        public void Rewrite_OneFalseRules_RuleResult()
        {
            var emptyNodes = Enumerable.Empty<ISyntacticNode>();
            var tree = MockRepository.GenerateStub<ISyntacticNode>();
            tree.Stub(t => t.Nodes).Return(emptyNodes);
            tree.Stub(t => t.Rewrite(Arg<IEnumerable<ISyntacticNode>>.Is.Anything))
                .Return(tree);

            var tree1 = MockRepository.GenerateStub<ISyntacticNode>();

            var rule1 = MockRepository.GenerateStub<ISyntaxRewriter>();
            rule1.Stub(r => r.Filter(tree)).Return(false);
            rule1.Stub(r => r.Visit(Arg<ISyntacticNode>.Is.Anything,
                Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(tree1);

            var rules = new[] {rule1};
            Assert.That(tree.Rewrite(rules), Is.SameAs(tree));
        }

        [Test]
        public void Rewriter_TwoTrueRules_LastRuleResult()
        {
            var tree = MockRepository.GenerateStub<ISyntacticNode>();
            tree.Stub(t => t.Nodes).Return(Enumerable.Empty<ISyntacticNode>());
            tree.Stub(t => t.Rewrite(Arg<IEnumerable<ISyntacticNode>>.Is.Anything))
                .Return(tree);

            var tree1 = MockRepository.GenerateStub<ISyntacticNode>();
            tree1.Stub(t => t.Nodes).Return(Enumerable.Empty<ISyntacticNode>());
            tree1.Stub(t => t.Rewrite(Arg<IEnumerable<ISyntacticNode>>.Is.Anything))
                .Return(tree1);

            var rule1 = MockRepository.GenerateStub<ISyntaxRewriter>();
            rule1.Stub(r => r.Filter(tree)).Return(true);
            rule1.Stub(r => r.Visit(Arg<ISyntacticNode>.Is.Anything,
                Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(tree1);

            var tree2 = MockRepository.GenerateStub<ISyntacticNode>();

            var rule2 = MockRepository.GenerateStub<ISyntaxRewriter>();
            rule2.Stub(r => r.Filter(tree1)).Return(true);
            rule2.Stub(r => r.Visit(Arg<ISyntacticNode>.Is.Anything,
                Arg<IEnumerable<ISyntacticNode>>.Is.Anything)).Return(tree2);

            var rules = new[] { rule1, rule2 };
            Assert.That(tree.Rewrite(rules), Is.SameAs(tree2));
        }

    }


}
