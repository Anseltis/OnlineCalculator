using System;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

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
        public void Resolve_ExistFunctionAndTargetArgumentCount_ResolvedTerm()
        {
            var children = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>()
            };

            var linkedClass = new LinkedLibrary(typeof(LinkedMath));
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
        public void Resolve_ExistFunctionAndLessArgumentCount_ResolvedTerm()
        {
            var children = Enumerable.Empty<ISyntacticNode>();

            var linkedClass = new LinkedLibrary(typeof(LinkedMath));
            var term = new FunctionDeclarationTerm("Max");

            var resolver = new ParamFunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);

        }

        [Test]
        public void Resolve_ExistFunctionAndMoreArgumentCount_Null()
        {
            var children = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
                MockRepository.GenerateStub<ISyntacticNode>()
            };

            var linkedClass = new LinkedLibrary(typeof(LinkedMath));
            var term = new FunctionDeclarationTerm("Max");

            var resolver = new ParamFunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Not.Null);
            Assert.That(resolvedTerm, Is.TypeOf<ParamFunctionTerm>());

            var functionTerm = (ParamFunctionTerm)resolvedTerm;
            Assert.That(functionTerm.ArgumentCount, Is.EqualTo(1));
        }

        [Test]
        public void Resolve_Function_Null()
        {
            var children = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
                MockRepository.GenerateStub<ISyntacticNode>()
            };

            var linkedClass = new LinkedLibrary(typeof(LinkedMath));
            var term = new FunctionDeclarationTerm("Sin");

            var resolver = new ParamFunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);
        }

        [Test]
        public void Resolve_AbsentFunction_Null()
        {
            var children = new[]
            {
                MockRepository.GenerateStub<ISyntacticNode>(),
            };

            var linkedClass = new LinkedLibrary(typeof(LinkedMath));
            var term = new FunctionDeclarationTerm("max");

            var resolver = new ParamFunctionResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);
        }



    }
}
