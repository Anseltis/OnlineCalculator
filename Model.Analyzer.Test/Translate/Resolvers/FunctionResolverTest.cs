using System;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;
using static AnsiSoft.Calculator.Model.ReflectionTool.ReflectionHelper;

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
                MockRepository.GenerateStub<ISyntacticNode>()
            };

            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            linkedClass.Stub(lc => lc.FindMethod("Sin", 1)).Return(MethodOf(() => LinkedMath.Sin(0)));
            var term = new FunctionDeclarationTerm("Sin");

            var resolver = new FunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Not.Null);
            Assert.That(resolvedTerm, Is.TypeOf<FunctionTerm>());

            var functionTerm = (FunctionTerm)resolvedTerm;
            var value = functionTerm.MethodInfo.Invoke(null, new object[] {Math.PI/2});
            Assert.That(value, Is.EqualTo(1.0).Within(1e-5));
        }
        

        [Test]
        public void Resolve_AbsentFunction_Null()
        {
            var children = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
            };

            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            linkedClass.Stub(lc => lc.FindMethod("sin", 1)).Return(null);
            var term = new FunctionDeclarationTerm("sin");

            var resolver = new FunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);
        }
    }
}
