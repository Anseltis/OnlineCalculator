using System;
using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Terms;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate
{
    [TestFixture]
    public class LinkerTest
    {
        /// <summary>
        /// Class for resolve external variable and function
        /// </summary>
        private static class LinkedMath
        {
            public static double Sin(double alpha) => Math.Sin(alpha);
            public static double Cos(double alpha) => Math.Cos(alpha);

            public static double Min(double first, params double[] args) =>
                Enumerable.Repeat(first, 1).Concat(args).Min();

            public static double Max(double first, params double[] args) =>
                Enumerable.Repeat(first, 1).Concat(args).Max();

            public static double PI { get; }

            static LinkedMath()
            {
                PI = Math.PI;
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Rewriter_Null_Null()
        {
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var linker = new Linker(null, linkedClass);
        }

        [Test]
        public void Rewriter_Empty_Empty()
        {
            var rules = new Dictionary<IResolverType, IEnumerable<IResolver>>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var linker = new Linker(rules, linkedClass);
            Assert.That(linker.Rules.Any(), Is.False);
        }

        [Test]
        public void Rewriter_SomeList_SameCount()
        {
            var rules = new Dictionary<IResolverType, IEnumerable<IResolver>>()
            {
                [MockRepository.GenerateStub<IResolverType>()] = new[]
                {
                    MockRepository.GenerateStub<IResolver>(),
                    MockRepository.GenerateStub<IResolver>()
                },
                [MockRepository.GenerateStub<IResolverType>()] = new[]
                {
                    MockRepository.GenerateStub<IResolver>(),
                }
            };
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var linker = new Linker(rules, linkedClass);
            Assert.That(linker.Rules.Count(), Is.EqualTo(2));
        }

        [Test]
        [ExpectedException(typeof(TranslateException))]
        public void CheckResult_NonTermNode_ThrowException()
        {
            var rules = new Dictionary<IResolverType, IEnumerable<IResolver>>();
            var node = new BlockSyntacticNode(
                MockRepository.GenerateStub<IBlock>(), 
                Enumerable.Empty<ISyntacticNode>());
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var linker = new Linker(rules, linkedClass);
            linker.CheckResult(node);
        }

        [Test]
        [ExpectedException(typeof(TranslateException))]
        public void CheckResult_NonLinkedTermNode_ThrowException()
        {
            var rules = new Dictionary<IResolverType, IEnumerable<IResolver>>();
            var node = new TermSyntacticNode(
                MockRepository.GenerateStub<ITerm>(),
                Enumerable.Empty<ISyntacticNode>());
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var linker = new Linker(rules, linkedClass);
            linker.CheckResult(node);
        }

        [Test]
        public void CheckResult_LinkedTermNode_DoesNotError()
        {
            var rules = new Dictionary<IResolverType, IEnumerable<IResolver>>();
            var node = new TermSyntacticNode(
                MockRepository.GenerateStub<IResolvedTerm>(),
                Enumerable.Empty<ISyntacticNode>());
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var linker = new Linker(rules, linkedClass);
            Assert.DoesNotThrow(() => linker.CheckResult(node));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLinkedClass_ThrowException()
        {
            var rules = new Dictionary<IResolverType, IEnumerable<IResolver>>();
            new Linker(rules, null);
        }
    }
}
