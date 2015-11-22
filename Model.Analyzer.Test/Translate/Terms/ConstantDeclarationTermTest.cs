using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Terms
{
    [TestFixture]
    class ConstantDeclarationTermTest
    {
        [Test]
        public void Variable_SomeValue_Equal()
        {
            const string variable = "myconst";
            var term = new ConstantDeclarationTerm(variable);
            Assert.That(term.Identifier, Is.EqualTo(variable));
        }
    }
}
