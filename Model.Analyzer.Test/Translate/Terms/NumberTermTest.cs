using System;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class NumberTermTest
    {
        [Test]
        public void Number_SomeValue_Equal()
        {
            var number = 3.7;
            var term = new NumberTerm(number);
            Assert.That(term.Number, Is.EqualTo(number));
        }

        [Test]
        public void CreateExpression_TwoChildExpressionAndPlusOperator_Sum()
        {
            var children = new Expression[] { };
            var term = new NumberTerm(10.0);

            var expression = term.CreateExpression(children);
            var lambda = Expression.Lambda<Func<double>>(expression);

            Assert.That(lambda.Compile()(), Is.EqualTo(10.0).Within(1e-5));
        }

    }
}
