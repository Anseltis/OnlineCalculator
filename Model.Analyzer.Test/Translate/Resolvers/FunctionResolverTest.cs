using System;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Terms;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Resolvers
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class FunctionResolverTest
    {
        /// <summary>
        /// Class for resolve external variable and function
        /// </summary>
        private static class LinkedMath
        {
            public static double Sin(double alpha) => Math.Sin(alpha);
        }


        [Test]
        public void Resolve_ExistFunction_ResolvedTerm()
        {
            var children = new[]
{
                MockRepository.GenerateStub<ISyntacticNode>(),
            };
            var linkedLibrary = MockRepository.GenerateStub<ILinkedLibrary>();
            var declarationTerm = MockRepository.GenerateStub<IFunctionDeclarationTerm>();

            var resolvedTerm = MockRepository.GenerateStub<IResolvedTerm>();
            linkedLibrary.Stub(lc => lc.FindFunction(declarationTerm)).Return(resolvedTerm);
            var resolver = new FunctionResolver();
            Assert.That(resolver.Resolve(declarationTerm, children, linkedLibrary), Is.Not.Null);

        }


        [Test]
        public void Resolve_AbsentFunction_Null()
        {
            var children = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
            };

            var linkedLibrary = MockRepository.GenerateStub<ILinkedLibrary>();
            var declarationTerm = MockRepository.GenerateStub<IFunctionDeclarationTerm>();

            linkedLibrary.Stub(lc => lc.FindFunction(declarationTerm)).Return(null);
            var resolver = new FunctionResolver();
            Assert.That(resolver.Resolve(declarationTerm, children, linkedLibrary), Is.Null);
        }
    }
}
