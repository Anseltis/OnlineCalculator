using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Operators
{
    [TestFixture]
    class UnaryPlusOperatorTest
    {
        [Test]
        public void CreateExpression_Onexpressions_UnaryPlus()
        {
            var operand = Expression.Constant(10.0);
            var op = new UnaryPlusOperator();
            var resExpression = op.CreateExpression(operand);
            var lambda = Expression.Lambda<Func<double>>(resExpression);
            Assert.That(lambda.Compile()(), Is.EqualTo(10.0).Within(1e-5));
        }

    }
}
