using System;
using NUnit.Framework;
using static AnsiSoft.Calculator.Model.ReflectionTool.ReflectionHelper;

namespace AnsiSoft.Calculator.Model.ReflectionTool.Test
{
    [TestFixture]
    class ReflectionHelperTest
    {
        private static class SomeClass
        {
            public static double SomeProperty { get; } = 8;
            public static double SomeMethod(double x) => 6;
        }

        [Test]
        public void PropertyOf_ExpressionWithProperty_TargetProperty()
        {
            var propertyInfo = PropertyOf(() => SomeClass.SomeProperty);
            Assert.That(propertyInfo, Is.Not.Null);
            var value = propertyInfo.GetValue(null);
            Assert.That(value, Is.EqualTo(8).Within(1e-3));
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void PropertyOf_WrongExpression_ThrowException()
        {
            var propertyInfo = PropertyOf(() => 1);
        }

        [Test]
        public void MethodyOf_ExpressionWithMethod_TargetMethod()
        {
            var methodInfo = MethodOf(() => SomeClass.SomeMethod(0));
            Assert.That(methodInfo, Is.Not.Null);
            var value = methodInfo.Invoke(null, new object[] {2.0});
            Assert.That(value, Is.EqualTo(6).Within(1e-3));
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException))]
        public void MethodOf_WrongExpression_ThrowException()
        {
            var methodInfo = MethodOf(() => 0);
        }

        [Test]
        [TestCase(typeof(SomeClass), true)]
        [TestCase(typeof(Math), true)]
        [TestCase(typeof(string), false)]
        public void IsStatic_SomeClass_TargetResult(Type type, bool result)
        {
            Assert.That(type.IsStatic(), Is.EqualTo(result));
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void IsStatic_Null_TargetResult()
        {
            Assert.That(ReflectionHelper.IsStatic(null), Is.EqualTo(0));
        }

    }
}
