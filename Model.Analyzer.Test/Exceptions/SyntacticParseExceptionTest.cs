using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Exceptions
{
    [TestFixture]
    [Category("Syntactic analyzer")]
    class SyntacticParseExceptionTest
    {
        [Test]
        public void Message_Expression_ErrorMessage()
        {
            var exception = new SyntacticParseException("2+3");
            Assert.That(exception.Message, Is.EqualTo("Syntactic error in '2+3'"));
        }
    }
}
