using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Operators
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class PlusOperatorTest
    {
        [Test]
        public void CreateExpression_TwoExpressions_Plus()
        {
            var left = Expression.Constant(10.0);
            var right = Expression.Constant(2.0);
            var op = new PlusOperator();
            var resExpression = op.CreateExpression(left, right);
            var lambda = Expression.Lambda<Func<double>>(resExpression);
            Assert.That(lambda.Compile()(), Is.EqualTo(12.0).Within(1e-5));
        }
    }
}
