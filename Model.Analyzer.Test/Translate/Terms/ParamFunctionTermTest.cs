using System;
using System.Linq;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class ParamFunctionTermTest
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
        public void MethodInfo_SomeMethod_Same()
        {
            var methodInfo = typeof(LinkedMath).GetMethods()
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.Max));
            var term = new ParamFunctionTerm(methodInfo, 1);

            Assert.That(term.MethodInfo, Is.SameAs(methodInfo));
        }

        [Test]
        public void ArgumentCount_SomeCount_Same()
        {
            var methodInfo = typeof(LinkedMath).GetMethods()
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.Max));
            var term = new ParamFunctionTerm(methodInfo, 1);

            Assert.That(term.ArgumentCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateExpression_SomeMethodInfoAndTargetrgs_Result()
        {
            var methodInfo = typeof (LinkedMath).GetMethods()
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.Max));
            var term = new ParamFunctionTerm(methodInfo, 1);

            var children = new Expression[] {Expression.Constant(1.0),};

            var expression = term.CreateExpression(children);
            var lambda = Expression.Lambda<Func<double>>(expression);

            Assert.That(lambda.Compile()(), Is.EqualTo(1.0).Within(1e-5));
        }

        [Test]
        public void CreateExpression_SomeMethodInfoAndMoreTargetArgs_Result()
        {
            var methodInfo = typeof(LinkedMath).GetMethods()
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.Max));
            var term = new ParamFunctionTerm(methodInfo, 1);

            var children = new Expression[] { Expression.Constant(1.0), Expression.Constant(2.0), Expression.Constant(3.0), };

            var expression = term.CreateExpression(children);
            var lambda = Expression.Lambda<Func<double>>(expression);

            Assert.That(lambda.Compile()(), Is.EqualTo(3.0).Within(1e-5));
        }

    }
}
