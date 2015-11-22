using System.Runtime.InteropServices;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Exceptions
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
    }
}
