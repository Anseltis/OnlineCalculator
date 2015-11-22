using System;
using System.Linq;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    public class FunctionTermTest
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
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.Sin));

            var term = new FunctionTerm(methodInfo);            
            Assert.That(term.MethodInfo, Is.SameAs(methodInfo));
        }

        [Test]
        public void CreateExpression_SomeMethodInfoAndArgs_Result()
        {
            var methodInfo = typeof(LinkedMath).GetMethods()
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.Sin));

            var term = new FunctionTerm(methodInfo);
            var children = new Expression[] { Expression.Constant(Math.PI/2), };

            var expression = term.CreateExpression(children);
            var lambda = Expression.Lambda<Func<double>>(expression);

            Assert.That(lambda.Compile()(), Is.EqualTo(1.0).Within(1e-5));
        }

    }
}
