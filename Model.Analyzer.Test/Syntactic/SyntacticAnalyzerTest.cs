﻿using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.ParseResult;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Syntactic
{
    [TestFixture]
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
