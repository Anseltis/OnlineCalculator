using System;
using System.Linq;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    public class ConstantTermTest
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
        public void PropertyInfo_SomePropertyInfo_Same()
        {
            var propertyInfo = typeof(LinkedMath).GetProperties()
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.PI));
            var term = new ConstantTerm(propertyInfo);
            Assert.That(term.PropertyInfo, Is.SameAs(propertyInfo));
        }

        [Test]
        public void CreateExpression_SomePropertyInfo_PropertyValue()
        {
            var propertyInfo = typeof(LinkedMath).GetProperties()
                .FirstOrDefault(p => p.Name == nameof(LinkedMath.PI));

            var term = new ConstantTerm(propertyInfo);
            var children = new Expression[] { };

            var expression = term.CreateExpression(children);
            var lambda = Expression.Lambda<Func<double>>(expression);

            Assert.That(lambda.Compile()(), Is.EqualTo(Math.PI).Within(1e-5));
        }

    }
}
