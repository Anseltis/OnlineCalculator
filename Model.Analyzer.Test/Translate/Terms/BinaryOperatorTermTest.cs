using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    class BinaryOperatorTermTest
    {
        [Test]
        public void Operator_SomeOperator_AsSame()
        {
            var op = MockRepository.GenerateStub<IBinaryOperator>();
            var term = new BinaryOperatorTerm(op);
            Assert.That(term.Operator, Is.SameAs(op));
        }

        [Test]
        public void CreateExpression_TwoChildExpressionAndPlusOperator_Add()
        {
            var left = Expression.Constant(10.0);
            var right = Expression.Constant(2.0);
            var result = Expression.Add(left, right);

            var op = MockRepository.GenerateStub<IBinaryOperator>();
            op.Stub(o => o.CreateExpression(left, right)).Return(result);
            var children = new Expression[] {left, right};
            var term = new BinaryOperatorTerm(op);
            Assert.That(term.CreateExpression(children), Is.SameAs(result));
        }
    }
}
