﻿using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Terms;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Rewriter
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class ResolverSyntaxRewriterTest
    {
        [Test]
        public void Constructor_AllArguments_AllProperties()
        {
            var resolverType = MockRepository.GenerateStub<IResolverType>();
            var resolvers = new[]
            {
                MockRepository.GenerateStub<IResolver>(),
                MockRepository.GenerateStub<IResolver>()
            };
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var rewriter = new ResolverSyntaxRewriter(resolverType, resolvers, linkedClass);
            Assert.That(rewriter.ResolverType, Is.SameAs(resolverType));
            Assert.That(rewriter.Resolvers, Is.SameAs(resolvers));
            Assert.That(rewriter.LinkedLibrary, Is.SameAs(linkedClass));
        }

        [Test]
        public void Filter_TermAndTrueResolve_True()
        {
            var term = MockRepository.GenerateStub<IDeclarationTerm>();
            var node = new TermSyntacticNode(term, Enumerable.Empty<ISyntacticNode>());

            var resolverType = MockRepository.GenerateStub<IResolverType>();
            resolverType.Stub(rt => resolverType.Resolve(term)).Return(true);
            var resolvers = Enumerable.Empty<IResolver>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var rewriter = new ResolverSyntaxRewriter(resolverType, resolvers, linkedClass);
            Assert.That(rewriter.Filter(node), Is.True);
        }

        [Test]
        public void Filter_TermAndFalseResolve_False()
        {
            var term = MockRepository.GenerateStub<IDeclarationTerm>();
            var node = new TermSyntacticNode(term, Enumerable.Empty<ISyntacticNode>());

            var resolverType = MockRepository.GenerateStub<IResolverType>();
            resolverType.Stub(rt => resolverType.Resolve(term)).Return(false);
            var resolvers = Enumerable.Empty<IResolver>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var rewriter = new ResolverSyntaxRewriter(resolverType, resolvers, linkedClass);
            Assert.That(rewriter.Filter(node), Is.False);
        }

        [Test]
        public void Filter_NotTerm_False()
        {
            var node = MockRepository.GenerateStub<ISyntacticNode>();
            var resolverType = MockRepository.GenerateStub<IResolverType>();
            var resolvers = Enumerable.Empty<IResolver>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var rewriter = new ResolverSyntaxRewriter(resolverType, resolvers, linkedClass);
            Assert.That(rewriter.Filter(node), Is.False);
        }

        [Test]
        [ExpectedException(typeof(CannotResolveIdentifierException))]
        public void Visit_TermAndDoesNotResolver_Throw()
        {
            var term = MockRepository.GenerateStub<IDeclarationTerm>();
            var node = new TermSyntacticNode(term, Enumerable.Empty<ISyntacticNode>());

            var resolverType = MockRepository.GenerateStub<IResolverType>();
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var resolvers = Enumerable.Empty<IResolver>();

            var rewriter = new ResolverSyntaxRewriter(resolverType, resolvers, linkedClass);
            rewriter.Visit(node, children);
        }

        [Test]
        [ExpectedException(typeof(CannotResolveIdentifierException))]
        public void Visit_TermAndNullResolve_Throw()
        {
            var term = MockRepository.GenerateStub<IDeclarationTerm>();
            var node = new TermSyntacticNode(term, Enumerable.Empty<ISyntacticNode>());

            var resolverType = MockRepository.GenerateStub<IResolverType>();
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();

            var resolver = MockRepository.GenerateStub<IResolver>();
            resolver.Stub(r => r.Resolve(term, children, linkedClass)).Return(null);


            var resolvers = new[] {resolver};

            var rewriter = new ResolverSyntaxRewriter(resolverType, resolvers, linkedClass);
            rewriter.Visit(node, children);
        }

        [Test]
        public void Visit_TermAndSuccessResolve_ResolvingNode()
        {
            var term = MockRepository.GenerateStub<IDeclarationTerm>();
            var node = new TermSyntacticNode(term, Enumerable.Empty<ISyntacticNode>());

            var resolverType = MockRepository.GenerateStub<IResolverType>();
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();

            var linkedTerm = MockRepository.GenerateStub<IResolvedTerm>();
            var resolver = MockRepository.GenerateStub<IResolver>();
            resolver.Stub(r => r.Resolve(term, children, linkedClass)).Return(linkedTerm);


            var resolvers = new[] { resolver };

            var rewriter = new ResolverSyntaxRewriter(resolverType, resolvers, linkedClass);
            var result = rewriter.Visit(node, children);
            Assert.That(result,Is.Not.Null);
            Assert.That(result, Is.TypeOf<TermSyntacticNode>());
            var termNode = (TermSyntacticNode) result;
            Assert.That(termNode.Term, Is.SameAs(linkedTerm));
        }
    }
}
