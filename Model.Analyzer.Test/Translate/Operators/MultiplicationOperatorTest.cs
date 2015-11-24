using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Operators
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class MultiplicationOperatorTest
    {
        [Test]
        public void CreateExpression_TwoExpressions_Multiplication()
        {
            var left = Expression.Constant(10.0);
            var right = Expression.Constant(2.0);
            var op = new MultiplicationOperator();
            var resExpression = op.CreateExpression(left, right);
            var lambda = Expression.Lambda<Func<double>>(resExpression);
            Assert.That(lambda.Compile()(), Is.EqualTo(20.0).Within(1e-5));
        }

    }
}
