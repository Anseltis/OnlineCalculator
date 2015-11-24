using AnsiSoft.Calculator.Model.Analyzer.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Exceptions
{
    [TestFixture]
    [Category("Analyzer exceptions")]
    public class CannotResolveIdentifierExceptionTest
    {
        [Test]
        public void Identifier_SomeIdentifier_Same()
        {
            const string identifier = "myFunc";
            var exception = new CannotResolveIdentifierException(identifier);
            Assert.That(exception.Identifier, Is.EqualTo(identifier));
        }

        [Test]
        public void Message_SomeIdentifier_CannotResolve()
        {
            var exception = new CannotResolveIdentifierException("myFunc");
            Assert.That(exception.Message, Is.EqualTo("Unresolves identifier 'myFunc' (or wrong signature)"));
        }
    }
}
