using System;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
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
    public class ParamFunctionResolverTest
    {
        /// <summary>
        /// Class for resolve external variable and function
        /// </summary>
        private static class LinkedMath
        {
            public static double Max(double first, params double[] args) =>
                Enumerable.Repeat(first, 1).Concat(args).Max();
        }

        
        [Test]
        public void Resolve_ExistFunctionAndTargetArgumentCount_ResolvedTerm()
        {
            var children = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>()
            };

            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            linkedClass.Stub(lc => lc.FindParamMethod("Max", 1)).Return(MethodOf(() => LinkedMath.Max(0)));
            var term = new FunctionDeclarationTerm("Max");

            var resolver = new ParamFunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Not.Null);
            Assert.That(resolvedTerm, Is.TypeOf<ParamFunctionTerm>());

            var functionTerm = (ParamFunctionTerm)resolvedTerm;
            Assert.That(functionTerm.ArgumentCount, Is.EqualTo(1));
            var value = functionTerm.MethodInfo.Invoke(null, new object[] {1.0, new double[] {}});
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
            linkedClass.Stub(lc => lc.FindParamMethod("max", 1)).Return(null);
            var term = new FunctionDeclarationTerm("max");

            var resolver = new ParamFunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);
        }



    }
}
