using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    public class UnaryOperatorTermTest
    {
        [Test]
        public void Operator_SomeOperator_AsSame()
        {
            var op = MockRepository.GenerateStub<IUnaryOperator>();
            var term = new UnaryOperatorTerm(op);
            Assert.That(term.Operator, Is.SameAs(op));
        }

        [Test]
        public void CreateExpression_OneChildExpressionAndMinusOperator_Negate()
        {
            var operand = Expression.Constant(10.0);
            var result = Expression.Negate(operand);

            var op = MockRepository.GenerateStub<IUnaryOperator>();
            op.Stub(o => o.CreateExpression(operand)).Return(result);
            var children = new Expression[] { operand };
            var term = new UnaryOperatorTerm(op);

            Assert.That(term.CreateExpression(children), Is.SameAs(result));
        }

    }
}
