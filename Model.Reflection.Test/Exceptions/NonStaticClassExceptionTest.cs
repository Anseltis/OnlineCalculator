using AnsiSoft.Calculator.Model.Reflection.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Reflection.Test.Exceptions
{
    [TestFixture]
    public class NonStaticClassExceptionTest
    {
        [Test]
        public void Message_SomeType_RightMessage()
        {
            var type = GetType();
            var exception = new NonStaticClassException(type);
            Assert.That(exception.Message, Is.EqualTo($"Class {type.FullName} is not static."));
        }

        [Test]
        public void Message_NoType_RightMessage()
        {
            var exception = new NonStaticClassException();
            Assert.That(exception.Message, Is.EqualTo($"There no static class"));
        }

    }
}
