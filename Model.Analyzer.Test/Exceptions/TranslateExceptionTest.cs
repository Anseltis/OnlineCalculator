using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Exceptions
{
    [TestFixture]
    [Category("Translator linker compiler")]
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
