using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;
using AnsiSoft.Calculator.Model.Interface.Transit;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate
{
    [TestFixture]
    public class TranslatorTest
    {
        [Test]
        public void Rules_Null_Null()
        {
            var translator = new Translator(null);
            Assert.That(translator.Rules, Is.Null);
        }

        [Test]
        public void Rules_Empty_Empty()
        {
            var rules = Enumerable.Empty<ISyntaxRewriter>();
            var translator = new Translator(rules);
            Assert.That(translator.Rules, Is.SameAs(rules));
        }

        [Test]
        public void Rules_SomeList_Same()
        {
            var rules = new[]
            {
                MockRepository.GenerateStub<ISyntaxRewriter>(),
                MockRepository.GenerateStub<ISyntaxRewriter>()
            };
            var translator = new Translator(rules);
            Assert.That(translator.Rules, Is.SameAs(rules));
        }

        [Test]
        public void CheckResult_TermNode_DoesNotThrow()
        {
            var node = new TermSyntacticNode(
                MockRepository.GenerateStub<ITerm>(),
                Enumerable.Empty<ISyntacticNode>());
            var translator = new Translator(Enumerable.Empty<ISyntaxRewriter>());
            Assert.DoesNotThrow(() => translator.CheckResult(node));
        }

        [Test]
        [ExpectedException(typeof(TranslateException))]
        public void CheckResult_BlockNode_Throw()
        {
            var node = new BlockSyntacticNode(
                MockRepository.GenerateStub<IBlock>(),
                Enumerable.Empty<ISyntacticNode>());
            var translator = new Translator(Enumerable.Empty<ISyntaxRewriter>());
            translator.CheckResult(node);
        }
    }
}
