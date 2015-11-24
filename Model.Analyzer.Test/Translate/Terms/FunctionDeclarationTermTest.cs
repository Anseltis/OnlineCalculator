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
            var term = new FunctionDeclarationTerm(function);
            Assert.That(term.Identifier, Is.EqualTo(function));
        }

    }
}
