using System;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Exceptions
{
    [TestFixture]
    [Category("Translator linker compiler")]
    class RuntimeCalculatorExceptionTest
    {
        [Test]
        public void InnerException_SomeException_Same()
        {
            var innerException = new Exception("Some error");
            var exception = new RuntimeCalculatorException(innerException);
            Assert.That(exception.InnerException, Is.SameAs(innerException));
        }

        [Test]
        public void Message_SomeException_RuntimeErrorMessage()
        {
            var innerException = new Exception("Some error");
            var exception = new RuntimeCalculatorException(innerException);
            Assert.That(exception.Message, Is.EqualTo("Runtime exception: Some error."));
        }
    }
}
