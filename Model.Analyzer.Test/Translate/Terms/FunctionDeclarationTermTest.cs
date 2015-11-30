using System;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    [Category("Translator linker compiler")]
    class FunctionDeclarationTermTest
    {
        [Test]
        public void Function_SomeValue_Equal()
        {
            const string function = "myfunc";
            var term = new FunctionDeclarationTerm(function, 1);
            Assert.That(term.Identifier, Is.EqualTo(function));
            Assert.That(term.ArgumentCount, Is.EqualTo(1));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullIdentifier_ThrowException()
        {
            new FunctionDeclarationTerm(null, 1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ZeroCountAgument_ThrowException()
        {
            new FunctionDeclarationTerm("function", 0);
        }

    }
}
