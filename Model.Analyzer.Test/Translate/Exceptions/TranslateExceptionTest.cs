using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Exceptions
{
    [TestFixture]
    public class TranslateExceptionTest
    {
        [Test]
        public void Constructor_DoNothing_Message()
        {
            var exception = new TranslateException();
            Assert.That(exception.Message, Is.EqualTo("Translate error"));
        }
    }
}
