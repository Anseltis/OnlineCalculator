using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Operators
{
    [TestFixture]
    public class DivisionOperatorTest
    {
        [Test]
        public void CreateExpression_TwoExpressions_Division()
        {
            var left = Expression.Constant(10.0);
            var right = Expression.Constant(2.0);
            var op = new DivisionOperator();
            var resExpression = op.CreateExpression(left, right);
            var lambda = Expression.Lambda<Func<double>>(resExpression);
            Assert.That(lambda.Compile()(), Is.EqualTo(5.0).Within(1e-5));
        }
    }
}
