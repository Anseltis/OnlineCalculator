using System;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Resolvers
{
    [TestFixture]
    public class ConstantResolverTest
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
        public void Resolve_ExistConstant_ResolvedTerm()
        {
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = new LinkedLibrary(typeof (LinkedMath));
            var term = new ConstantDeclarationTerm("PI");

            var resolver = new ConstantResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Not.Null);
            Assert.That(resolvedTerm, Is.TypeOf<ConstantTerm>());

            var constantTerm = (ConstantTerm) resolvedTerm;
            Assert.That(constantTerm.PropertyInfo.GetValue(null), Is.EqualTo(Math.PI).Within(1e-5));
        }

        [Test]
        public void Resolve_AbsentConstant_Null()
        {
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = new LinkedLibrary(typeof(LinkedMath));
            var term = new ConstantDeclarationTerm("Pi");

            var resolver = new ConstantResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);
        }

        [Test]
        public void Resolve_Function_Null()
        {
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = new LinkedLibrary(typeof(LinkedMath));
            var term = new ConstantDeclarationTerm("Sin");

            var resolver = new ConstantResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);
        }

    }
}
